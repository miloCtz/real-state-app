using Mapster;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using RealEstateApp.ApiService.Mappings;

namespace RealEstateApp.ApiService.Tests;

public class TestFixture
{
    public IMapper Mapper { get; }
    public Mock<ILogger<object>> LoggerMock { get; }

    public TestFixture()
    {
        // Configure Mapster mappings
        var config = new TypeAdapterConfig();
        new MappingConfig().Register(config);
        config.Compile();

        // Create Mapper instance for tests
        var services = new ServiceCollection();
        services.AddSingleton(config);
        services.AddScoped<IMapper, ServiceMapper>();
        var serviceProvider = services.BuildServiceProvider();
        Mapper = serviceProvider.GetRequiredService<IMapper>();

        // Mock logger
        LoggerMock = new Mock<ILogger<object>>();
    }
}