using RealEstateApp.Domain.Common;
using RealEstateApp.Domain.Entities;

namespace RealEstateApp.Domain.Repositories;

public interface IPropertyRepository
{
    Task<Property?> GetPropertyAsync(string id);
    Task<PagedResult<Property>> GetPropertiesAsync(PropertyFilter filter);
}