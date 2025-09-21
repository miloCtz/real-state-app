using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MongoDB.Driver;
using RealEstateApp.Domain.Entities;
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
        services.AddSingleton<IPropertyRepository, PropertyRepository>();
    }
    
    public static async Task SeedMongo(this IServiceProvider serviceProvider)
    {
        var dbContext = serviceProvider.GetRequiredService<MongoDbContext>();
        
        var propertyCount = await dbContext.Properties.CountDocumentsAsync(Builders<Property>.Filter.Empty);
        if (propertyCount >= 20)
        {
            return;
        }
        var owner = await EnsureOwnerExistsAsync(dbContext);
        
        var properties = GenerateProperties(20, owner);
        await dbContext.Properties.InsertManyAsync(properties);
    }
    
    private static async Task<Owner> EnsureOwnerExistsAsync(MongoDbContext dbContext)
    {
        var owner = await dbContext.Owners.Find(Builders<Owner>.Filter.Empty).FirstOrDefaultAsync();
        
        if (owner == null)
        {
            owner = new Owner
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Sample Owner",
                Address = "123 Main Street, City, Country",
                Birthday = DateTime.Now.AddYears(-30)
            };
            
            await dbContext.Owners.InsertOneAsync(owner);
        }
        
        return owner;
    }
    
    private static List<Property> GenerateProperties(int count, Owner owner)
    {
        var random = new Random();
        var properties = new List<Property>();
        
        string[] streetNames = { "Maple", "Oak", "Pine", "Elm", "Cedar", "Birch", "Willow", "Cypress" };
        string[] streetTypes = { "Street", "Avenue", "Boulevard", "Lane", "Drive", "Road", "Place", "Way" };
        string[] cities = { "New York", "Los Angeles", "Chicago", "Houston", "Phoenix", "Philadelphia", "San Antonio", "San Diego" };
        string[] states = { "NY", "CA", "IL", "TX", "AZ", "PA", "TX", "CA" };
        
        for (int i = 0; i < count; i++)
        {
            var streetName = streetNames[random.Next(streetNames.Length)];
            var streetType = streetTypes[random.Next(streetTypes.Length)];
            var city = cities[random.Next(cities.Length)];
            var state = states[random.Next(states.Length)];
            var streetNumber = random.Next(100, 9999);
            
            var property = new Property
            {
                Id = Guid.NewGuid().ToString(),
                Name = $"{streetName} {streetType} Property",
                Address = $"{streetNumber} {streetName} {streetType}, {city}, {state}",
                Price = random.Next(100000, 1000000),
                CodeInternal = $"PROP-{i + 1:D4}",
                Year = random.Next(1950, 2023),
                IdOwner = owner.Id,
                Owner = owner
            };
            
            // Add some property images
            var imageCount = random.Next(2, 4);
            for (int j = 0; j < imageCount; j++)
            {
                property.PropertyImages.Add(new PropertyImage
                {
                    IdProperty = int.Parse(property.Id.Substring(0, 8), System.Globalization.NumberStyles.HexNumber) % 1000000,
                    File = $"property_{random.Next(1, 20)}.jpg",
                    Enabled = true
                });
            }
            
            // Add property trace
            property.PropertyTraces.Add(new PropertyTrace
            {
                DateSale = DateTime.Now.AddDays(-random.Next(1, 365)),
                Name = $"Sale Record #{i + 1}",
                Value = property.Price * 0.95m,
                Tax = property.Price * 0.05m
            });
            
            properties.Add(property);
        }
        
        return properties;
    }
}