using Aspire.Hosting.Orleans;

IDistributedApplicationBuilder builder = DistributedApplication.CreateBuilder(args);

OrleansService orleans = builder.AddOrleans("default"); // Add Orleans service -> name for diagnostic purposes

builder.Build().Run();