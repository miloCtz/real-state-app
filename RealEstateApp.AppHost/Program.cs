var builder = DistributedApplication.CreateBuilder(args);

var apiService = builder.AddProject<Projects.RealEstateApp_ApiService>("weatherapi");

builder.AddNpmApp("react", "../RealEstateApp.ReactApp")
    .WithReference(apiService)
    .WithEnvironment("BROWSER", "none")
    .WithHttpEndpoint(env: "VITE_PORT")
    .WithExternalHttpEndpoints()
    .PublishAsDockerFile();

builder.Build().Run();