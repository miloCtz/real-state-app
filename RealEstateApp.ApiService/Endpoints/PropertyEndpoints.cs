using Microsoft.AspNetCore.Http;
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
    /// Maps all property-related endpoints to the web application.
    /// </summary>
    /// <param name="app">The web application to map the endpoints to.</param>
    public static void MapPropertyEndpoints(this WebApplication app)
    {
        // Define the GET /api/properties endpoint for retrieving a filtered, paginated list of properties
        app.MapGet("/api/properties", async (
            [AsParameters] PropertyFilter filter,
            IPropertyRepository propertyRepository,
            MapsterMapper.IMapper mapper,
            ILogger logger) =>
        {
            logger.LogInformation("Getting properties with filter: Name={Name}, Address={Address}, MinPrice={MinPrice}, MaxPrice={MaxPrice}, Page={Page}, PageSize={PageSize}",
                filter.Name, filter.Address, filter.MinPrice, filter.MaxPrice, filter.PageNumber, filter.PageSize);

            var properties = await propertyRepository.GetPropertiesAsync(filter);
            var propertyListDto = mapper.Map<PropertyListDto>(properties);
            
            logger.LogInformation("Retrieved {Count} properties of {TotalCount} total", properties.Items.Count(), properties.TotalCount);
            return Results.Ok(propertyListDto);
        })
        .WithName("GetProperties")
        .WithTags("Properties")
        .WithOpenApi(operation => {
            operation.Summary = "Get a paginated list of properties";
            operation.Description = "Returns a paginated list of properties with optional filtering by name, address, price range, and pagination parameters.";
            operation.Responses["200"].Description = "Success - Returns a paginated list of properties";
            operation.Responses["400"].Description = "Bad Request - Invalid filter parameters";
            operation.Responses["500"].Description = "Internal Server Error - An unexpected error occurred";
            return operation;
        })
        .Produces<PropertyListDto>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status500InternalServerError);

        // Define the GET /api/properties/{id} endpoint for retrieving a specific property by ID
        app.MapGet("/api/properties/{id}", async (
            string id,
            IPropertyRepository propertyRepository,
            MapsterMapper.IMapper mapper,
            ILogger logger) =>
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
        })
        .WithName("GetProperty")
        .WithTags("Properties")
        .WithOpenApi(operation => {
            operation.Summary = "Get property by ID";
            operation.Description = "Returns detailed information about a specific property by its unique identifier.";
            operation.Responses["200"].Description = "Success - Returns the property information";
            operation.Responses["404"].Description = "Not Found - Property with the specified ID was not found";
            operation.Responses["500"].Description = "Internal Server Error - An unexpected error occurred";
            return operation;
        })
        .Produces<PropertyDto>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .ProducesProblem(StatusCodes.Status500InternalServerError);
    }
}