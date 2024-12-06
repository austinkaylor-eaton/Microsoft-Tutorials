IDistributedApplicationBuilder builder = DistributedApplication.CreateBuilder(args);

IResourceBuilder<RedisResource> cache = builder.AddRedis("cache");

builder.AddProject<Projects.API>("api")
    .WithReference(cache);

// After adding all resources, run the app...

builder.Build().Run();