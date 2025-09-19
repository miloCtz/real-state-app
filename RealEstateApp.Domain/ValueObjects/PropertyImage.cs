namespace RealEstateApp.Domain.Entities
{
    public record PropertyImage
    {        
        public int IdProperty { get; init; }        
        public required string File { get; init; }
        public bool Enabled { get; init; }
    }
}