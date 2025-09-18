var builder = DistributedApplication.CreateBuilder(args);

var apiService = builder.AddProject<Projects.RealEstateApp_ApiService>("apiservice");

builder.AddProject<Projects.RealEstateApp_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithReference(apiService);

builder.Build().Run();
