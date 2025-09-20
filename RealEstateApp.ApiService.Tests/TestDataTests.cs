using FluentAssertions;

namespace RealEstateApp.ApiService.Tests;

public class TestDataTests
{
    [Fact]
    public void GenerateProperty_ShouldCreateValidPropertyObject()
    {
        // Arrange & Act
        var property = TestData.GenerateProperty("test123");
        
        // Assert
        property.Should().NotBeNull();
        property.Id.Should().Be("test123");
        property.Name.Should().NotBeNullOrEmpty();
        property.Address.Should().NotBeNullOrEmpty();
        property.Owner.Should().NotBeNull();
        property.PropertyImages.Should().NotBeNull();
    }
    
    [Fact]
    public void GenerateProperties_ShouldCreateSpecifiedNumberOfProperties()
    {
        // Arrange & Act
        var properties = TestData.GenerateProperties(5);
        
        // Assert
        properties.Should().NotBeNull();
        properties.Should().HaveCount(5);
        properties.Should().AllSatisfy(p => 
        {
            p.Should().NotBeNull();
            p.Id.Should().NotBeNullOrEmpty();
            p.Owner.Should().NotBeNull();
            p.Owner.Id.Should().NotBeNullOrEmpty();
            int.TryParse(p.Owner.Id, out _).Should().BeTrue("Owner ID should be parseable as int");
        });
    }
    
    [Fact]
    public void GeneratePagedProperties_ShouldCreateValidPagedResult()
    {
        // Arrange & Act
        var pagedResult = TestData.GeneratePagedProperties(totalCount: 25, pageSize: 10, pageNumber: 2);
        
        // Assert
        pagedResult.Should().NotBeNull();
        pagedResult.TotalCount.Should().Be(25);
        pagedResult.PageSize.Should().Be(10);
        pagedResult.PageNumber.Should().Be(2);
        pagedResult.TotalPages.Should().Be(3); // Calculated based on TotalCount and PageSize
        pagedResult.Items.Should().NotBeEmpty();
    }
    
    [Fact]
    public void GeneratePropertyFilter_ShouldCreateValidFilter()
    {
        // Arrange & Act
        var filter = TestData.GeneratePropertyFilter();
        
        // Assert
        filter.Should().NotBeNull();
        filter.PageNumber.Should().BeGreaterThan(0);
        filter.PageSize.Should().BeGreaterThan(0);
    }
}