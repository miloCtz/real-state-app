using Mapster;
using RealEstateApp.ApiService.Dtos;
using RealEstateApp.Domain.Common;
using RealEstateApp.Domain.Entities;

namespace RealEstateApp.ApiService.Mappings;

public class MappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        // Property to PropertyDto mapping
        config.NewConfig<Property, PropertyDto>()
            .Map(dest => dest.Images, src => src.PropertyImages);
            
        // PagedResult<Property> to PropertyListDto mapping
        config.NewConfig<PagedResult<Property>, PropertyListDto>()
            .Map(dest => dest.Items, src => src.Items)
            .Map(dest => dest.TotalCount, src => src.TotalCount)
            .Map(dest => dest.PageNumber, src => src.PageNumber)
            .Map(dest => dest.PageSize, src => src.PageSize)
            .Map(dest => dest.TotalPages, src => src.TotalPages);
    }
}