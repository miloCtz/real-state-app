using System;
using System.Collections.Generic;

namespace RealEstateApp.Domain.Entities
{
    public class Property : IEntity
    {
        public Property()
        {
            PropertyImages = new List<PropertyImage>();
            PropertyTraces = new List<PropertyTrace>();
        }

        public string Id { get; set; } = default!;
        public required string Name { get; set; }
        public required string Address { get; set; }
        public decimal Price { get; set; }
        public required string CodeInternal { get; set; }
        public int Year { get; set; }
        
        // Foreign key for Owner
        public int IdOwner { get; set; }
        
        // Navigation properties
        public virtual Owner Owner { get; set; } = null!;
        public virtual ICollection<PropertyImage> PropertyImages { get; set; }
        public virtual ICollection<PropertyTrace> PropertyTraces { get; set; }
    }
}