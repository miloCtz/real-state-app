using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Events;
using System.Reflection;

namespace RealEstateApp.Infrastructure;

public static class ServiceExtensions
{
    /// <summary>
    /// Adds and configures Serilog to the application
    /// </summary>
    public static WebApplicationBuilder AddSerilog(this WebApplicationBuilder builder)
    {
        // Create a bootstrap logger for startup errors
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
            .Enrich.FromLogContext()
            .Enrich.WithMachineName()
            .Enrich.WithEnvironmentName()
            .WriteTo.Console()
            .WriteTo.File(
                path: "logs/realestate-api-.log",
                rollingInterval: RollingInterval.Day,
                outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}")
            .CreateBootstrapLogger();

        // Configure Serilog with configuration from appsettings
        builder.Host.UseSerilog((context, services, configuration) => configuration
            .ReadFrom.Configuration(context.Configuration)
            .ReadFrom.Services(services)
            .Enrich.FromLogContext()
            .Enrich.WithMachineName()
            .Enrich.WithEnvironmentName()
            .WriteTo.Console()
            .WriteTo.File(
                path: "logs/realestate-api-.log",
                rollingInterval: RollingInterval.Day,
                outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}"));

        return builder;
    }

    /// <summary>
    /// Adds and configures Swagger/OpenAPI to the application
    /// </summary>
    public static WebApplicationBuilder AddSwagger(this WebApplicationBuilder builder)
    {
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

        return builder;
    }

    /// <summary>
    /// Adds and configures Mapster for object mapping
    /// </summary>
    public static WebApplicationBuilder AddMapster(this WebApplicationBuilder builder, Assembly assembly)
    {
        var typeAdapterConfig = TypeAdapterConfig.GlobalSettings;
        typeAdapterConfig.Scan(assembly);
        builder.Services.AddSingleton(typeAdapterConfig);
        builder.Services.AddScoped<IMapper, ServiceMapper>();

        return builder;
    }
    
    /// <summary>
    /// Adds and configures Mapster for object mapping with multiple assemblies
    /// </summary>
    public static WebApplicationBuilder AddMapster(this WebApplicationBuilder builder, params Assembly[] assemblies)
    {
        var typeAdapterConfig = TypeAdapterConfig.GlobalSettings;
        
        foreach (var assembly in assemblies)
        {
            typeAdapterConfig.Scan(assembly);
        }
        
        builder.Services.AddSingleton(typeAdapterConfig);
        builder.Services.AddScoped<IMapper, ServiceMapper>();

        return builder;
    }

    /// <summary>
    /// Configures the Swagger middleware
    /// </summary>
    public static WebApplication UseSwaggerUI(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Real Estate API v1");
                c.RoutePrefix = "swagger"; // Serve Swagger UI at /swagger
            });
        }

        return app;
    }

    /// <summary>
    /// Configures Serilog request logging
    /// </summary>
    public static WebApplication UseSerilogLogging(this WebApplication app)
    {
        app.UseSerilogRequestLogging();
        return app;
    }
}