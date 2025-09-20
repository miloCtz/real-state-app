using FluentAssertions;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RealEstateApp.ApiService.Dtos;
using RealEstateApp.ApiService.Endpoints;
using RealEstateApp.Domain.Common;
using RealEstateApp.Domain.Repositories;
using System.Net;

namespace RealEstateApp.ApiService.Tests.Endpoints;

public class PropertyEndpointsTests : IClassFixture<TestFixture>
{
    private readonly Mock<IPropertyRepository> _propertyRepositoryMock;
    private readonly TestFixture _fixture;
    
    public PropertyEndpointsTests(TestFixture fixture)
    {
        _fixture = fixture;
        _propertyRepositoryMock = new Mock<IPropertyRepository>();
    }
    
    [Fact]
    public async Task GetProperties_ReturnsOkResultWithPagedList()
    {
        // Arrange
        var filter = TestData.GeneratePropertyFilter();
        var pagedResult = TestData.GeneratePagedProperties();
        
        _propertyRepositoryMock
            .Setup(r => r.GetPropertiesAsync(It.IsAny<PropertyFilter>()))
            .ReturnsAsync(pagedResult);
            
        // Act
        var result = await PropertyEndpoints.GetPropertiesHandler(
            filter,
            _propertyRepositoryMock.Object,
            _fixture.Mapper,
            _fixture.LoggerMock.Object);
            
        // Assert
        var okResult = result.Should().BeOfType<Ok<PropertyListDto>>().Subject;
        var value = okResult.Value;
        
        value.Should().NotBeNull();
        value!.Items.Should().HaveCount(pagedResult.Items.Count());
        value.TotalCount.Should().Be(pagedResult.TotalCount);
        value.PageNumber.Should().Be(pagedResult.PageNumber);
        value.PageSize.Should().Be(pagedResult.PageSize);
        value.TotalPages.Should().Be(pagedResult.TotalPages);
        
        _propertyRepositoryMock.Verify(r => r.GetPropertiesAsync(filter), Times.Once);
    }
    
    [Fact]
    public async Task GetProperty_WithValidId_ReturnsOkResult()
    {
        // Arrange
        var propertyId = "prop123";
        var property = TestData.GenerateProperty(propertyId);
        
        _propertyRepositoryMock
            .Setup(r => r.GetPropertyAsync(propertyId))
            .ReturnsAsync(property);
            
        // Act
        var result = await PropertyEndpoints.GetPropertyHandler(
            propertyId,
            _propertyRepositoryMock.Object,
            _fixture.Mapper,
            _fixture.LoggerMock.Object);
            
        // Assert
        var okResult = result.Should().BeOfType<Ok<PropertyDto>>().Subject;
        var value = okResult.Value;
        
        value.Should().NotBeNull();
        value.Id.Should().Be(propertyId);
        value.Name.Should().Be(property.Name);
        value.Address.Should().Be(property.Address);
        value.Price.Should().Be(property.Price);
        
        _propertyRepositoryMock.Verify(r => r.GetPropertyAsync(propertyId), Times.Once);
    }
    
    [Fact]
    public async Task GetProperty_WithInvalidId_ReturnsNotFoundResult()
    {
        // Arrange
        var propertyId = "nonExistentId";
        
        _propertyRepositoryMock
            .Setup(r => r.GetPropertyAsync(propertyId))
            .ReturnsAsync((Domain.Entities.Property?)null);
            
        // Act
        var result = await PropertyEndpoints.GetPropertyHandler(
            propertyId,
            _propertyRepositoryMock.Object,
            _fixture.Mapper,
            _fixture.LoggerMock.Object);
            
        // Assert
        var notFoundResult = result.Should().BeOfType<NotFound<ProblemDetails>>().Subject;
        notFoundResult.Value.Should().NotBeNull();
        notFoundResult.Value!.Status.Should().Be((int)HttpStatusCode.NotFound);
        notFoundResult.Value.Detail.Should().Contain(propertyId);
        
        _propertyRepositoryMock.Verify(r => r.GetPropertyAsync(propertyId), Times.Once);
    }
}