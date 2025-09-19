namespace RealEstateApp.Domain.Repositories;
using RealEstateApp.Domain.Entities;


public interface IPropertyRepository
{
    Task<Property?> GetPropertyAsync(string id);
    Task<IEnumerable<Property>> GetPropertiesAsync();
}