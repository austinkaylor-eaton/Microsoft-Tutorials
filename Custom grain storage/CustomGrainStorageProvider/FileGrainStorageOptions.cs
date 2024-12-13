using Orleans.Storage;

namespace CustomGrainStorageProvider;

/// <summary>
/// Options class for configuring the <see cref="FileGrainStorage"/>
/// </summary>
public sealed class FileGrainStorageOptions : IStorageProviderSerializerOptions
{
    /// <summary>
    /// The root directory where the grain state files are persisted
    /// </summary>
    public required string RootDirectory { get; set; }

    public required IGrainStorageSerializer GrainStorageSerializer { get; set; }
}