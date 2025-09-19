using MongoDB.Driver;
using RealEstateApp.Domain.Entities;

namespace RealEstateApp.Persistence;

public class MongoDbContext
{
    private readonly IMongoDatabase _database;

    public MongoDbContext(IMongoClient mongoClient, MongoSettings mongoSettings) => _database = mongoClient.GetDatabase(mongoSettings.DatabaseName);

    public IMongoCollection<Owner> Owners => _database.GetCollection<Owner>("Owners");
    public IMongoCollection<Property> Properties => _database.GetCollection<Property>("Properties");
}