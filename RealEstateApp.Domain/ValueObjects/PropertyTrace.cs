using System;

namespace RealEstateApp.Domain.Entities
{
    public record PropertyTrace
    {
        public DateTime DateSale { get; set; }
        public required string Name { get; set; }
        public decimal Value { get; set; }
        public decimal Tax { get; set; }
    }
}