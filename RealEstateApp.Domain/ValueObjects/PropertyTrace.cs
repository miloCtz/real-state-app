using System;

namespace RealEstateApp.Domain.Entities
{
    public record PropertyTrace
    {
        public DateTime DateSale { get; init; }
        public required string Name { get; init; }
        public decimal Value { get; init; }
        public decimal Tax { get; init; }
    }
}