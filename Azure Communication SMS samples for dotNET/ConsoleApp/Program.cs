using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using SynchronousConsoleClient;

HostApplicationBuilderSettings settings = new()
{
    Args = args,
    Configuration = new ConfigurationManager(),
    ContentRootPath = Directory.GetCurrentDirectory(),
};

settings.Configuration.AddUserSecrets<Program>();

HostApplicationBuilder builder = Host.CreateApplicationBuilder(settings);

builder.Services.AddOptions<AzureCommunicationServiceOptions>()
    .BindConfiguration(AzureCommunicationServiceOptions.ConfigurationSectionName) // 👈 Bind the section
    .ValidateDataAnnotations() // 👈 Enable validation
    .ValidateOnStart(); // 👈 Validate on start

IHost host = builder.Build();

AzureCommunicationServiceOptions azureCommunicationServiceOptions = host.Services.GetRequiredService<IOptions<AzureCommunicationServiceOptions>>().Value;

AzureCommunicationServiceSmsClient client = new(azureCommunicationServiceOptions);
client.SendMessageToPhoneNumber("+14127132060", "Hello, World!");

