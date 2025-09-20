using FluentAssertions;
using Mapster;
using MapsterMapper;
using RealEstateApp.ApiService.Dtos;
using RealEstateApp.ApiService.Mappings;

namespace RealEstateApp.ApiService.Tests.Mappings;

public class MappingTests
{
    private readonly IMapper _mapper;
    
    public MappingTests()
    {
        // Configure Mapster
        var config = new TypeAdapterConfig();
        new MappingConfig().Register(config);
        config.Compile();
        _mapper = new Mapper(config);
    }
    
    [Fact]
    public void Property_To_PropertyDto_Mapping_ShouldMapCorrectly()
    {
        // Arrange
        var property = TestData.GenerateProperty();
        
        // Act
        var propertyDto = _mapper.Map<PropertyDto>(property);
        
        // Assert
        propertyDto.Should().NotBeNull();
        propertyDto.Id.Should().Be(property.Id);
        propertyDto.Name.Should().Be(property.Name);
        propertyDto.Address.Should().Be(property.Address);
        propertyDto.Price.Should().Be(property.Price);
        propertyDto.CodeInternal.Should().Be(property.CodeInternal);
        propertyDto.Year.Should().Be(property.Year);
        propertyDto.IdOwner.Should().Be(property.IdOwner);
        
        // Check Owner mapping
        propertyDto.Owner.Should().NotBeNull();
        propertyDto.Owner!.Name.Should().Be(property.Owner.Name);
        propertyDto.Owner.Address.Should().Be(property.Owner.Address);
        
        // Check PropertyImages mapping
        propertyDto.Images.Should().NotBeNull();
        propertyDto.Images.Should().HaveCount(property.PropertyImages.Count);
        propertyDto.Images.First().File.Should().Be(property.PropertyImages.First().File);
    }
    
    [Fact]
    public void PagedResult_To_PropertyListDto_Mapping_ShouldMapCorrectly()
    {
        // Arrange
        var pagedResult = TestData.GeneratePagedProperties(totalCount: 25, pageSize: 10, pageNumber: 2);
        
        // Act
        var propertyListDto = _mapper.Map<PropertyListDto>(pagedResult);
        
        // Assert
        propertyListDto.Should().NotBeNull();
        propertyListDto.TotalCount.Should().Be(pagedResult.TotalCount);
        propertyListDto.PageNumber.Should().Be(pagedResult.PageNumber);
        propertyListDto.PageSize.Should().Be(pagedResult.PageSize);
        propertyListDto.TotalPages.Should().Be(pagedResult.TotalPages);
        propertyListDto.Items.Should().HaveCount(pagedResult.Items.Count());
        
        // Verify first item mapped correctly
        var firstProperty = pagedResult.Items.First();
        var firstDto = propertyListDto.Items.First();
        
        firstDto.Id.Should().Be(firstProperty.Id);
        firstDto.Name.Should().Be(firstProperty.Name);
        firstDto.Address.Should().Be(firstProperty.Address);
    }
}