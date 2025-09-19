namespace RealEstateApp.Domain.Entities;

public class Owner : IEntity
{
    public Owner()
    {
    }

    public string Id { get; set; } = default!;
    public required string Name { get; init; }
    public required string Address { get; init; }
    public string? Photo { get; init; }
    public DateTime Birthday { get; init; }
}
