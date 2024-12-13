using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Orleans.Configuration.Overrides;
using Orleans.Storage;

namespace CustomGrainStorageProvider;

/// <summary>
/// Allows you to scope the options to the provider name and at the same time create an instance of the <see cref="FileGrainStorage"/> to ease the registration to the service collection
/// </summary>
internal static class FileGrainStorageFactory
{
    internal static IGrainStorage Create(
        IServiceProvider services, string name)
    {
        var optionsMonitor =
            services.GetRequiredService<IOptionsMonitor<FileGrainStorageOptions>>();

        return ActivatorUtilities.CreateInstance<FileGrainStorage>(
            services,
            name,
            optionsMonitor.Get(name),
            services.GetProviderClusterOptions(name));
    }
}