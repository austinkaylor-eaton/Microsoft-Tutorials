IDistributedApplicationBuilder builder = DistributedApplication.CreateBuilder(args);

IResourceBuilder<RedisResource> cache = builder.AddRedis("cache");

IResourceBuilder<SqlServerServerResource> sql = builder.AddSqlServer("sql")
    .WithLifetime(ContainerLifetime.Persistent);

IResourceBuilder<SqlServerDatabaseResource> db = sql.AddDatabase("database");

builder.AddProject<Projects.API>("api")
    .WithReference(cache)
    .WithReference(db)
    .WaitFor(db);
   
// After adding all resources, run the app...

builder.Build().Run();