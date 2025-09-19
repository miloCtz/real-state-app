using RealEstateApp.ApiService.Dtos;
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
    builder.AddMapster(Assembly.GetExecutingAssembly(), typeof(RealEstateApp.Infrastructure.ServiceExtensions).Assembly);
    
    // Add service defaults & Aspire components.
    builder.AddServiceDefaults();

    // Add services to the container.
    builder.Services.AddProblemDetails();

    //Add MongoDb Client
    builder.AddMongo();

    var app = builder.Build();

    if (app.Environment.IsDevelopment())
    {
        await app.Services.SeedMongo();    
    }

    // Configure the HTTP request pipeline.
    app.UseExceptionHandler();
    
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
        logger.LogInformation("Getting properties with filter: Name={Name}, Address={Address}, MinPrice={MinPrice}, MaxPrice={MaxPrice}, Page={Page}, PageSize={PageSize}",
            filter.Name, filter.Address, filter.MinPrice, filter.MaxPrice, filter.PageNumber, filter.PageSize);

        var properties = await propertyRepository.GetPropertiesAsync(filter);
        var propertyListDto = mapper.Map<PropertyListDto>(properties);
        
        logger.LogInformation("Retrieved {Count} properties of {TotalCount} total", properties.Items.Count(), properties.TotalCount);
        return Results.Ok(propertyListDto);
    })
    .WithName("GetProperties")
    .WithTags("Properties")
    .WithOpenApi();

    // GET: api/properties/{id}
    app.MapGet("/api/properties/{id}", async (
        string id,
        IPropertyRepository propertyRepository,
        MapsterMapper.IMapper mapper,
        ILogger<Program> logger) =>
    {
        logger.LogInformation("Getting property with ID: {PropertyId}", id);
        
        var property = await propertyRepository.GetPropertyAsync(id);

        if (property is null)
        {
            logger.LogWarning("Property with ID {PropertyId} not found", id);
            return Results.NotFound();
        }

        logger.LogInformation("Retrieved property: {PropertyName}, Address: {PropertyAddress}", property.Name, property.Address);
        var propertyDto = mapper.Map<PropertyDto>(property);
        return Results.Ok(propertyDto);
    })
    .WithName("GetProperty")
    .WithTags("Properties")
    .WithOpenApi();

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