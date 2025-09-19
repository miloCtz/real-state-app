using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RealEstateApp.Domain.Repositories;
using RealEstateApp.Persistence.Repositories;

namespace RealEstateApp.Persistence;

public static class ServiceExtensions
{
    public static void AddMongo(this WebApplicationBuilder builder)
    {
        var configuration = builder.Configuration;
        var services = builder.Services;
        var mongoSettings = configuration.GetSection("MongoSettings").Get<MongoSettings>();
        builder.AddMongoDBClient(connectionName: "mongodb");
        services.AddSingleton(mongoSettings!);
        services.AddSingleton<MongoDbContext>();
        services.AddSingleton<IOwnerRepository, OwnerRepository>();
    }
}