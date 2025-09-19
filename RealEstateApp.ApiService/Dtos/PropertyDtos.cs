namespace RealEstateApp.ApiService.Dtos;

public class PropertyDto
{
    public string Id { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string Address { get; set; } = default!;
    public decimal Price { get; set; }
    public string CodeInternal { get; set; } = default!;
    public int Year { get; set; }
    public int IdOwner { get; set; }
    public OwnerDto? Owner { get; set; }
    public List<PropertyImageDto> Images { get; set; } = new();
}

public class OwnerDto
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string Address { get; set; } = default!;
    public string Photo { get; set; } = default!;
    public DateTime Birthday { get; set; }
}

public class PropertyImageDto
{
    public int Id { get; set; }
    public string File { get; set; } = default!;
    public bool Enabled { get; set; }
}

public class PropertyListDto
{
    public List<PropertyDto> Items { get; set; } = new();
    public int TotalCount { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalPages { get; set; }
}