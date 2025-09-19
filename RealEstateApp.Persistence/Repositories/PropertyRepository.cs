using MongoDB.Driver;
using RealEstateApp.Domain.Entities;
using RealEstateApp.Domain.Repositories;

namespace RealEstateApp.Persistence.Repositories;

public class PropertyRepository(MongoDbContext dbContext) : IPropertyRepository
{
    public async Task<IEnumerable<Property>> GetPropertiesAsync()
    {
        var projection = Builders<Property>.Projection
            .Include(p => p.Id)
            .Include(p => p.Name)
            .Include(p => p.Address)
            .Include(p => p.Price)
            .Include(p => p.IdOwner);

        return await dbContext.Properties.Find(_ => true)
            .Project<Property>(projection)
            .ToListAsync();
    }

    public async Task<Property?> GetPropertyAsync(string id)
    {
        return await dbContext.Properties.Find(p => p.Id == id).FirstOrDefaultAsync();
    }
}