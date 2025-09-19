namespace RealEstateApp.Domain.Repositories;
using RealEstateApp.Domain.Entities;

public interface IOwnerRepository
{
    Task<Owner?> GetOwnerAsync(string id);
}