using MongoDB.Driver;
using RealEstateApp.Domain.Common;
using RealEstateApp.Domain.Entities;
using RealEstateApp.Domain.Repositories;

namespace RealEstateApp.Persistence.Repositories;

public class PropertyRepository(MongoDbContext dbContext) : IPropertyRepository
{    
    public async Task<PagedResult<Property>> GetPropertiesAsync(PropertyFilter filter)
    {
        var filterBuilder = Builders<Property>.Filter;
        var filterDefinition = filterBuilder.Empty;
        
        if (!string.IsNullOrWhiteSpace(filter.Name))
        {
            filterDefinition &= filterBuilder.Regex(p => p.Name, 
                new MongoDB.Bson.BsonRegularExpression(filter.Name, "i"));
        }
        
        if (!string.IsNullOrWhiteSpace(filter.Address))
        {
            filterDefinition &= filterBuilder.Regex(p => p.Address, 
                new MongoDB.Bson.BsonRegularExpression(filter.Address, "i"));
        }
        
        if (filter.MinPrice.HasValue)
        {
            filterDefinition &= filterBuilder.Gte(p => p.Price, filter.MinPrice.Value);
        }
        
        if (filter.MaxPrice.HasValue)
        {
            filterDefinition &= filterBuilder.Lte(p => p.Price, filter.MaxPrice.Value);
        }

        var projection = Builders<Property>.Projection
            .Include(p => p.Id)
            .Include(p => p.Name)
            .Include(p => p.Address)
            .Include(p => p.Price)
            .Include(p => p.IdOwner)
            .Include(p => p.PropertyImages.FirstOrDefault());
        
        var totalCount = await dbContext.Properties.CountDocumentsAsync(filterDefinition);
        
        var skip = (filter.PageNumber - 1) * filter.PageSize;
        
        var properties = await dbContext.Properties
            .Find(filterDefinition)
            .Project<Property>(projection)
            .Skip(skip)
            .Limit(filter.PageSize)
            .ToListAsync();
        
        return new PagedResult<Property>
        {
            Items = properties,
            TotalCount = (int)totalCount,
            PageNumber = filter.PageNumber,
            PageSize = filter.PageSize
        };
    }

    public async Task<Property?> GetPropertyAsync(string id)
    {
        return await dbContext.Properties.Find(p => p.Id == id).FirstOrDefaultAsync();
    }
}