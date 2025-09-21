namespace RealEstateApp.Domain.Entities
{
    public class Property : IEntity
    {
        public Property()
        {
        }

        public string Id { get; set; } = default!;
        public required string Name { get; init; }
        public required string Address { get; init; }
        public decimal Price { get; init; }
        public required string CodeInternal { get; init; }
        public int Year { get; init; }

        // Foreign key for Owner
        public string IdOwner { get; init; }

        // Navigation properties
        public virtual Owner Owner { get; set; } = null!;
        public virtual ICollection<PropertyImage> PropertyImages { get; set; } = [];
        public virtual ICollection<PropertyTrace> PropertyTraces { get; set; } = [];
    }
}