using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Orleans;
using Orleans.Hosting;
using Orleans.Runtime;
using Orleans.Storage;

namespace CustomGrainStorageProvider;

/// <summary>
///  internally registers the grain storage as a named service using AddSingletonNamedService, an extension provided by <see cref="Orleans.Core"/>
/// </summary>
public static class FileSiloBuilderExtensions
{
    public static ISiloBuilder AddFileGrainStorage(
        this ISiloBuilder builder,
        string providerName,
        Action<FileGrainStorageOptions> options) =>
        builder.ConfigureServices(
            services => services.AddFileGrainStorage(
                providerName, options));

    public static IServiceCollection AddFileGrainStorage(
        this IServiceCollection services,
        string providerName,
        Action<FileGrainStorageOptions> options)
    {
        services.AddOptions<FileGrainStorageOptions>(providerName)
            .Configure(options);

        services.AddTransient<
            IPostConfigureOptions<FileGrainStorageOptions>,
            DefaultStorageProviderSerializerOptionsConfigurator<FileGrainStorageOptions>>();
        
        return services.AddKeyedSingleton(providerName, FileGrainStorageFactory.Create())
            .AddKeyedSingleton<ILifecycleParticipant<ISiloLifecycle>>(
                providerName,
                (sp, n) => (ILifecycleParticipant<ISiloLifecycle>)sp.GetKeyedService<IGrainStorage>(n));
    }
}