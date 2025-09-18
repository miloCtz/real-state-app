var builder = DistributedApplication.CreateBuilder(args);

var mongo = builder.AddMongoDB("mongo")
                   .WithMongoExpress();

var mongodb = mongo.AddDatabase("mongodb");

var apiService = builder.AddProject<Projects.RealEstateApp_ApiService>("weatherapi")
    .WithReference(mongodb)
    .WaitFor(mongodb);

builder.AddNpmApp("react", "../RealEstateApp.ReactApp")
    .WithReference(apiService)
    .WaitFor(apiService)
    .WithEnvironment("BROWSER", "none")
    .WithHttpEndpoint(env: "VITE_PORT")
    .WithExternalHttpEndpoints()
    .PublishAsDockerFile() ;

builder.Build().Run();