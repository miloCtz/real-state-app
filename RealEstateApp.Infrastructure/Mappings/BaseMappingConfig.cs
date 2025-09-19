using Mapster;

namespace RealEstateApp.Infrastructure.Mappings;

/// <summary>
/// Base mapping configuration class that can be extended in different projects
/// </summary>
public abstract class BaseMappingConfig : IRegister
{
    public abstract void Register(TypeAdapterConfig config);
}