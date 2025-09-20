using RealEstateApp.ApiService.Dtos;
using RealEstateApp.ApiService.Endpoints;
using RealEstateApp.Domain.Common;
using RealEstateApp.Domain.Repositories;
using RealEstateApp.Infrastructure;
using RealEstateApp.Persistence;
using Serilog;
using System.Reflection;

try
{
    Log.Information("Starting Real Estate API");

    var builder = WebApplication.CreateBuilder(args);

    // Add custom infrastructure services
    builder.AddSerilog();
    builder.AddSwagger();
    builder.AddGlobalExceptionHandler();
    builder.AddMapster(Assembly.GetExecutingAssembly(), typeof(RealEstateApp.Infrastructure.ServiceExtensions).Assembly);

    // Add service defaults & Aspire components.
    builder.AddServiceDefaults();

    //Add MongoDb Client
    builder.AddMongo();

    var app = builder.Build();

    if (app.Environment.IsDevelopment())
    {
        await app.Services.SeedMongo();
    }

    // Configure the HTTP request pipeline.
    app.UseGlobalExceptionHandler();

    // Configure Swagger
    app.UseSwaggerUI();

    // Configure Serilog request logging
    app.UseSerilogLogging();

    // GET: api/properties (with optional filtering)
    app.MapGet("/api/properties", async (
        [AsParameters] PropertyFilter filter,
        IPropertyRepository propertyRepository,
        MapsterMapper.IMapper mapper,
        ILogger<Program> logger) =>
    {
        return await PropertyEndpoints.GetPropertiesHandler(filter, propertyRepository, mapper, logger);
    })
    .WithName("GetProperties")
    .WithTags("Properties")
    .WithOpenApi(operation =>
    {
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

    // GET: api/properties/{id}
    app.MapGet("/api/properties/{id}", async (
        string id,
        IPropertyRepository propertyRepository,
        MapsterMapper.IMapper mapper,
        ILogger<Program> logger) =>
    {
        return await PropertyEndpoints.GetPropertyHandler(id, propertyRepository, mapper, logger);
    })
    .WithName("GetProperty")
    .WithTags("Properties")
    .WithOpenApi(operation =>
    {
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

    app.MapDefaultEndpoints();

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}