using RealEstateApp.Domain.Common;
using RealEstateApp.Domain.Entities;

namespace RealEstateApp.ApiService.Tests;

public static class TestData
{
    public static Property GenerateProperty(string id = "prop123")
    {
        return new Property
        {
            Id = id,
            Name = "Test Property",
            Address = "123 Test Street",
            Price = 250000M,
            CodeInternal = "INT-001",
            Year = 2022,
            IdOwner = 1,
            Owner = new Owner
            {
                Id = "1",  // Use a numeric string for ID to match the int conversion in DTOs
                Name = "John Doe",
                Address = "456 Owner Street",
                Photo = "photo.jpg",
                Birthday = new DateTime(1980, 1, 1)
            },
            PropertyImages = new List<PropertyImage>
            {
                new PropertyImage
                {
                    Enabled = true,
                    File = "image1.jpg",
                    IdProperty = 1
                }
            }
        };
    }

    public static List<Property> GenerateProperties(int count)
    {
        var properties = new List<Property>();
        
        for (int i = 1; i <= count; i++)
        {
            properties.Add(new Property
            {
                Id = $"prop{i}",
                Name = $"Property {i}",
                Address = $"{i} Test Avenue",
                Price = 200000M + (i * 10000),
                CodeInternal = $"INT-00{i}",
                Year = 2020 + (i % 5),
                IdOwner = i % 3 + 1,
                Owner = new Owner
                {
                    Id = $"{i % 3 + 1}",  // Use a numeric string for ID to match the int conversion in DTOs
                    Name = $"Owner {i % 3 + 1}",
                    Address = $"{i} Owner Road",
                    Photo = $"photo{i}.jpg",
                    Birthday = new DateTime(1980, i % 12 + 1, i % 28 + 1)
                },
                PropertyImages = new List<PropertyImage>
                {
                    new PropertyImage
                    {
                        Enabled = true,
                        File = $"image{i}.jpg",
                        IdProperty = i
                    }
                }
            });
        }
        
        return properties;
    }

    public static PagedResult<Property> GeneratePagedProperties(int totalCount = 25, int pageSize = 10, int pageNumber = 1)
    {
        var itemsToTake = Math.Min(pageSize, totalCount - ((pageNumber - 1) * pageSize));
        var items = GenerateProperties(itemsToTake);
        
        return new PagedResult<Property>
        {
            Items = items,
            TotalCount = totalCount,
            PageNumber = pageNumber,
            PageSize = pageSize
        };
    }
    
    public static PropertyFilter GeneratePropertyFilter()
    {
        return new PropertyFilter
        {
            Name = "Test",
            Address = null,
            MinPrice = 200000,
            MaxPrice = 500000,
            PageNumber = 1,
            PageSize = 10
        };
    }
}