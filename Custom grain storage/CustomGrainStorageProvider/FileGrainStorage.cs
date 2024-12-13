using System.Globalization;
using Microsoft.Extensions.Options;
using Orleans;
using Orleans.Configuration;
using Orleans.Runtime;
using Orleans.Storage;

namespace CustomGrainStorageProvider;

/// <summary>
/// 
/// </summary>
/// <remarks>ILifecycleParticipant&lt;ISiloLifecycle&gt; allows you to subscribe to a particular event in the lifecycle of the silo</remarks>
public sealed class FileGrainStorage : IGrainStorage, ILifecycleParticipant<ISiloLifecycle>
{
    private readonly string _storageName;
    private readonly FileGrainStorageOptions _options;
    private readonly ClusterOptions _clusterOptions;

    public FileGrainStorage(
        string storageName,
        FileGrainStorageOptions options,
        IOptions<ClusterOptions> clusterOptions)
    {
        _storageName = storageName;
        _options = options;
        _clusterOptions = clusterOptions.Value;
    }

    /// <summary>
    /// Clear the state of a grain from file storage
    /// </summary>
    /// <param name="stateName"></param>
    /// <param name="grainId"></param>
    /// <param name="grainState"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public Task ClearStateAsync<T>(
        string stateName,
        GrainId grainId,
        IGrainState<T> grainState)
    {
        /*
         * Before proceeding to delete the file and reset the ETag, you check if the current ETag is the same as the last write time UTC
         */
        string fName = GetKeyString(stateName, grainId);
        string path = Path.Combine(_options.RootDirectory, fName!);
        FileInfo fileInfo = new FileInfo(path);
        if (!fileInfo.Exists) return Task.CompletedTask;
        if (fileInfo.LastWriteTimeUtc.ToString(CultureInfo.InvariantCulture) != grainState.ETag)
        {
            throw new InconsistentStateException($"""
                                                  Version conflict (ClearState): ServiceId={_clusterOptions.ServiceId}
                                                  ProviderName={_storageName} GrainType={typeof(T)}
                                                  GrainReference={grainId}.
                                                  """);
        }

        grainState.ETag = null;
        grainState.State = Activator.CreateInstance<T>()!;

        fileInfo.Delete();

        return Task.CompletedTask;
    }

    /// <summary>
    /// Read the state of a grain from file storage
    /// </summary>
    /// <param name="stateName"></param>
    /// <param name="grainId"></param>
    /// <param name="grainState"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public async Task ReadStateAsync<T>(
        string stateName,
        GrainId grainId,
        IGrainState<T> grainState)
    {
        // get the filename using the GetKeyString function and combine it with the root directory coming from the _options instance
        string fName = GetKeyString(stateName, grainId);
        string path = Path.Combine(_options.RootDirectory, fName);
        FileInfo fileInfo = new(path);
        if (fileInfo is { Exists: false })
        {
            grainState.State = Activator.CreateInstance<T>()!;
            return;
        }

        using StreamReader stream = fileInfo.OpenText();
        string storedData = await stream.ReadToEndAsync();
    
        grainState.State = _options.GrainStorageSerializer.Deserialize<T>(new BinaryData(storedData));
        // which will be used by other functions for inconsistency checks to prevent data loss
        grainState.ETag = fileInfo.LastWriteTimeUtc.ToString(CultureInfo.InvariantCulture);
    }

    /// <summary>
    /// Write the state of a grain to file storage
    /// </summary>
    /// <param name="stateName"></param>
    /// <param name="grainId"></param>
    /// <param name="grainState"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task WriteStateAsync<T>(
        string stateName,
        GrainId grainId,
        IGrainState<T> grainState)
    {
        /*
         * The current ETag is used to check against the last updated time in the UTC of the file.
         * If the date is different, it means that another activation of the same grain changed the state concurrently.
         * In this situation, you'll throw an InconsistentStateException, which will result in the current activation being killed to prevent overwriting the state previously saved by the other activated grain
         */
        BinaryData? storedData = _options.GrainStorageSerializer.Serialize(grainState.State);
        string fName = GetKeyString(stateName, grainId);
        string path = Path.Combine(_options.RootDirectory, fName);
        FileInfo fileInfo = new(path);
        if (fileInfo.Exists && fileInfo.LastWriteTimeUtc.ToString(CultureInfo.InvariantCulture) != grainState.ETag)
        {
            throw new InconsistentStateException($"""
                                                  Version conflict (WriteState): ServiceId={_clusterOptions.ServiceId}
                                                  ProviderName={_storageName} GrainType={typeof(T)}
                                                  GrainReference={grainId}.
                                                  """);
        }

        await File.WriteAllBytesAsync(path, storedData.ToArray());

        fileInfo.Refresh();
        grainState.ETag = fileInfo.LastWriteTimeUtc.ToString(CultureInfo.InvariantCulture);
    }

    /// <summary>
    /// Used to subscribe to the lifecycle of the silo
    /// </summary>
    /// <remarks>used to conditionally create the root directory to store the grains states when it doesn't already exist</remarks>
    /// <param name="lifecycle"></param>
    public void Participate(ISiloLifecycle lifecycle)
    {
        lifecycle.Subscribe(
            observerName: OptionFormattingUtilities.Name<FileGrainStorage>(_storageName),
            stage: ServiceLifecycleStage.ApplicationServices,
            onStart: _ =>
            {
                Directory.CreateDirectory(_options.RootDirectory);
                return Task.CompletedTask;
            });
    }
    
    /// <summary>
    /// Ensures filename uniqueness by combining the service ID, grain ID, and grain type
    /// </summary>
    /// <param name="grainType"></param>
    /// <param name="grainId"></param>
    /// <returns></returns>
    private string GetKeyString(string grainType, GrainId grainId) =>
        $"{_clusterOptions.ServiceId}.{grainId.Key}.{grainType}";
}