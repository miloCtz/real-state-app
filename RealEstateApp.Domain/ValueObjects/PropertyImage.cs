namespace RealEstateApp.Domain.Entities
{
    public record PropertyImage
    {        
        public int IdProperty { get; set; }        
        public required string File { get; set; }
        public bool Enabled { get; set; }
    }
}