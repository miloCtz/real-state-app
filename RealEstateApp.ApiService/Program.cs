using Mapster;
using MapsterMapper;
using Microsoft.OpenApi.Models;
using RealEstateApp.ApiService.Dtos;
using RealEstateApp.Domain.Common;
using RealEstateApp.Domain.Repositories;
using RealEstateApp.Persistence;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add service defaults & Aspire components.
builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddProblemDetails();

// Add Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo 
    { 
        Title = "Real Estate API", 
        Version = "v1",
        Description = "API for managing real estate properties",
        Contact = new OpenApiContact
        {
            Name = "Real Estate Team",
            Email = "support@realestate.example.com"
        }
    });
});

// Add Mapster
var typeAdapterConfig = TypeAdapterConfig.GlobalSettings;
typeAdapterConfig.Scan(Assembly.GetExecutingAssembly());
builder.Services.AddSingleton(typeAdapterConfig);
builder.Services.AddScoped<IMapper, ServiceMapper>();

//Add MongoDb Client
builder.AddMongo();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    await app.Services.SeedMongo();    
    
    app.UseSwagger();
    app.UseSwaggerUI(c => 
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Real Estate API v1");
        c.RoutePrefix = "swagger"; // Serve Swagger UI at /swagger
    });
}

// Configure the HTTP request pipeline.
app.UseExceptionHandler();

// GET: api/properties (with optional filtering)
app.MapGet("/api/properties", async (
    [AsParameters] PropertyFilter filter,
    IPropertyRepository propertyRepository,
    IMapper mapper) =>
{
    var properties = await propertyRepository.GetPropertiesAsync(filter);
    var propertyListDto = mapper.Map<PropertyListDto>(properties);
    return Results.Ok(propertyListDto);
})
.WithName("GetProperties")
.WithTags("Properties")
.WithOpenApi();

// GET: api/properties/{id}
app.MapGet("/api/properties/{id}", async (
    string id,
    IPropertyRepository propertyRepository,
    IMapper mapper) =>
{
    var property = await propertyRepository.GetPropertyAsync(id);

    if (property is null)
    {
        return Results.NotFound();
    }

    var propertyDto = mapper.Map<PropertyDto>(property);
    return Results.Ok(propertyDto);
})
.WithName("GetProperty")
.WithTags("Properties")
.WithOpenApi();


app.MapDefaultEndpoints();
app.Run();