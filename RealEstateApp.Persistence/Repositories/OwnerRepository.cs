using MongoDB.Driver;
using RealEstateApp.Domain.Entities;
using RealEstateApp.Domain.Repositories;

namespace RealEstateApp.Persistence.Repositories;

public class OwnerRepository(MongoDbContext dbContext) : IOwnerRepository
{
    public async Task<Owner?> GetOwnerAsync(string id)
    {
        return await dbContext.Owners.Find(o => o.Id == id).FirstOrDefaultAsync();
    }
}