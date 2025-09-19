using RealEstateApp.Domain.Common;
using RealEstateApp.Domain.Repositories;
using RealEstateApp.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add service defaults & Aspire components.
builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddProblemDetails();


//Add MongoDb Client
builder.AddMongo();

var app = builder.Build();

if(app.Environment.IsDevelopment())
{
    await app.Services.SeedMongo();
}



// Configure the HTTP request pipeline.
app.UseExceptionHandler();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", async (IPropertyRepository propertyRepository) =>
{

    var properties = await propertyRepository.GetPropertiesAsync(new PropertyFilter()
    {
        PageNumber = 1,
        PageSize = 5,
        Address = "Maple"
    });
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
});

app.MapDefaultEndpoints();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
