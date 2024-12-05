using Orleans;
using OrleansServer;
using ServiceDefaults;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder.AddKeyedAzureTableClient("clustering");
builder.UseOrleansClient();

WebApplication app = builder.Build();

app.MapGet("/counter/{grainId}", async (IClusterClient client, string grainId) =>
{
    var grain = client.GetGrain<ICounterGrain>(grainId);
    return await grain.Get();
});

app.MapPost("/counter/{grainId}", async (IClusterClient client, string grainId) =>
{
    var grain = client.GetGrain<ICounterGrain>(grainId);
    return await grain.Increment();
});

app.UseFileServer();

await app.RunAsync();