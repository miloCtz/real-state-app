using FluentAssertions;
using Moq;
using RealEstateApp.Domain.Common;
using RealEstateApp.Domain.Entities;
using RealEstateApp.Domain.Repositories;

namespace RealEstateApp.ApiService.Tests.Repositories;

public class PropertyRepositoryTests
{
    private readonly Mock<IPropertyRepository> _repositoryMock;
    
    public PropertyRepositoryTests()
    {
        _repositoryMock = new Mock<IPropertyRepository>();
    }
    
    [Fact]
    public async Task GetPropertyAsync_ShouldReturnProperty_WhenPropertyExists()
    {
        // Arrange
        var propertyId = "prop123";
        var expectedProperty = TestData.GenerateProperty(propertyId);
        
        _repositoryMock.Setup(r => r.GetPropertyAsync(propertyId))
            .ReturnsAsync(expectedProperty);
            
        // Act
        var property = await _repositoryMock.Object.GetPropertyAsync(propertyId);
        
        // Assert
        property.Should().NotBeNull();
        property!.Id.Should().Be(propertyId);
        property.Name.Should().Be(expectedProperty.Name);
        property.Address.Should().Be(expectedProperty.Address);
        property.Price.Should().Be(expectedProperty.Price);
        
        _repositoryMock.Verify(r => r.GetPropertyAsync(propertyId), Times.Once);
    }
    
    [Fact]
    public async Task GetPropertyAsync_ShouldReturnNull_WhenPropertyDoesNotExist()
    {
        // Arrange
        var propertyId = "nonExistentId";
        
        _repositoryMock.Setup(r => r.GetPropertyAsync(propertyId))
            .ReturnsAsync((Property?)null);
            
        // Act
        var property = await _repositoryMock.Object.GetPropertyAsync(propertyId);
        
        // Assert
        property.Should().BeNull();
        
        _repositoryMock.Verify(r => r.GetPropertyAsync(propertyId), Times.Once);
    }
    
    [Fact]
    public async Task GetPropertiesAsync_ShouldReturnPagedResults()
    {
        // Arrange
        var filter = TestData.GeneratePropertyFilter();
        var expectedResult = TestData.GeneratePagedProperties();
        
        _repositoryMock.Setup(r => r.GetPropertiesAsync(It.IsAny<PropertyFilter>()))
            .ReturnsAsync(expectedResult);
            
        // Act
        var result = await _repositoryMock.Object.GetPropertiesAsync(filter);
        
        // Assert
        result.Should().NotBeNull();
        result.Items.Should().HaveCount(expectedResult.Items.Count());
        result.TotalCount.Should().Be(expectedResult.TotalCount);
        result.PageNumber.Should().Be(expectedResult.PageNumber);
        result.PageSize.Should().Be(expectedResult.PageSize);
        
        _repositoryMock.Verify(r => r.GetPropertiesAsync(filter), Times.Once);
    }
}