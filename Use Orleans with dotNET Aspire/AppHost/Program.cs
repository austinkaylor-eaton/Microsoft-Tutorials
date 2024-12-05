using Aspire.Hosting.Azure;
using Aspire.Hosting.Orleans;

IDistributedApplicationBuilder builder = DistributedApplication.CreateBuilder(args);

// Add the resources which you will use for Orleans clustering and
// grain state storage.
IResourceBuilder<AzureStorageResource> storage = builder.AddAzureStorage("storage").RunAsEmulator();
IResourceBuilder<AzureTableStorageResource> clusteringTable = storage.AddTables("clustering");
IResourceBuilder<AzureBlobStorageResource> grainStorage = storage.AddBlobs("grain-state");

// Add the Orleans resource to the Aspire DistributedApplication
// builder, then configure it with Azure Table Storage for clustering
// and Azure Blob Storage for grain storage.
OrleansService? orleans = builder.AddOrleans("default")  // Add Orleans service -> name for diagnostic purposes
    .WithClustering(clusteringTable)
    .WithGrainStorage("Default", grainStorage);

// Add our server project and reference your 'orleans' resource from it.
// it can join the Orleans cluster as a service.
// This implicitly add references to the required resources.
// In this case, that is the 'clusteringTable' resource declared earlier.
builder.AddProject<Projects.OrleansServer>("silo")
    .WithReference(orleans)
    .WithReplicas(3);

// Reference the Orleans resource as a client from the 'frontend'
// project so that it can connect to the Orleans cluster.
builder.AddProject<Projects.OrleansClient>("frontend")
    .WithReference(orleans.AsClient())
    .WithExternalHttpEndpoints()
    .WithReplicas(3);

// Build and run the application.
await using DistributedApplication app = builder.Build();
await app.RunAsync();
