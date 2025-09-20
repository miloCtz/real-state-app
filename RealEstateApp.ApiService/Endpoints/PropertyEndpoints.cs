using Microsoft.AspNetCore.Mvc;
using RealEstateApp.ApiService.Dtos;
using RealEstateApp.Domain.Common;
using RealEstateApp.Domain.Repositories;
using System.Net;

namespace RealEstateApp.ApiService.Endpoints;

/// <summary>
/// Contains endpoint definitions for property-related API operations.
/// </summary>
public static class PropertyEndpoints
{
    /// <summary>
    /// Handler for the GET /api/properties endpoint
    /// </summary>
    public static async Task<IResult> GetPropertiesHandler(
        PropertyFilter filter,
        IPropertyRepository propertyRepository,
        MapsterMapper.IMapper mapper,
        ILogger logger)
    {
        logger.LogInformation("Getting properties with filter: Name={Name}, Address={Address}, MinPrice={MinPrice}, MaxPrice={MaxPrice}, Page={Page}, PageSize={PageSize}",
            filter.Name, filter.Address, filter.MinPrice, filter.MaxPrice, filter.PageNumber, filter.PageSize);

        var properties = await propertyRepository.GetPropertiesAsync(filter);
        var propertyListDto = mapper.Map<PropertyListDto>(properties);

        logger.LogInformation("Retrieved {Count} properties of {TotalCount} total", properties.Items.Count(), properties.TotalCount);
        return Results.Ok(propertyListDto);
    }

    /// <summary>
    /// Handler for the GET /api/properties/{id} endpoint
    /// </summary>
    public static async Task<IResult> GetPropertyHandler(
        string id,
        IPropertyRepository propertyRepository,
        MapsterMapper.IMapper mapper,
        ILogger logger)
    {
        logger.LogInformation("Getting property with ID: {PropertyId}", id);

        var property = await propertyRepository.GetPropertyAsync(id);

        if (property is null)
        {
            logger.LogWarning("Property with ID {PropertyId} not found", id);
            var notFoundProblem = new ProblemDetails
            {
                Status = (int)HttpStatusCode.NotFound,
                Title = "Resource not found",
                Detail = $"Property with ID {id} was not found.",
                Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.4"
            };

            return Results.NotFound(notFoundProblem);
        }

        logger.LogInformation("Retrieved property: {PropertyName}, Address: {PropertyAddress}", property.Name, property.Address);
        var propertyDto = mapper.Map<PropertyDto>(property);
        return Results.Ok(propertyDto);
    }
}