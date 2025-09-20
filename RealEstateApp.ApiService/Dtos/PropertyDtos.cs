namespace RealEstateApp.ApiService.Dtos;

/// <summary>
/// Data transfer object for property information
/// </summary>
public class PropertyDto
{
    /// <summary>
    /// Unique identifier of the property
    /// </summary>
    public string Id { get; set; } = default!;
    
    /// <summary>
    /// Name of the property
    /// </summary>
    public string Name { get; set; } = default!;
    
    /// <summary>
    /// Physical address of the property
    /// </summary>
    public string Address { get; set; } = default!;
    
    /// <summary>
    /// Price of the property
    /// </summary>
    public decimal Price { get; set; }
    
    /// <summary>
    /// Internal code used for property identification
    /// </summary>
    public string CodeInternal { get; set; } = default!;
    
    /// <summary>
    /// Year the property was built
    /// </summary>
    public int Year { get; set; }
    
    /// <summary>
    /// ID of the property owner
    /// </summary>
    public int IdOwner { get; set; }
    
    /// <summary>
    /// Information about the property owner
    /// </summary>
    public OwnerDto? Owner { get; set; }
    
    /// <summary>
    /// Collection of property images
    /// </summary>
    public List<PropertyImageDto> Images { get; set; } = new();
}

/// <summary>
/// Data transfer object for property owner information
/// </summary>
public class OwnerDto
{
    /// <summary>
    /// Unique identifier of the owner
    /// </summary>
    public int Id { get; set; }
    
    /// <summary>
    /// Name of the property owner
    /// </summary>
    public string Name { get; set; } = default!;
    
    /// <summary>
    /// Address of the property owner
    /// </summary>
    public string Address { get; set; } = default!;
    
    /// <summary>
    /// URL or reference to the owner's photo
    /// </summary>
    public string Photo { get; set; } = default!;
    
    /// <summary>
    /// Birth date of the property owner
    /// </summary>
    public DateTime Birthday { get; set; }
}

/// <summary>
/// Data transfer object for property image information
/// </summary>
public class PropertyImageDto
{
    /// <summary>
    /// Unique identifier of the image
    /// </summary>
    public int Id { get; set; }
    
    /// <summary>
    /// File name or URL of the image
    /// </summary>
    public string File { get; set; } = default!;
    
    /// <summary>
    /// Indicates whether the image is enabled/active
    /// </summary>
    public bool Enabled { get; set; }
}

/// <summary>
/// Data transfer object for a paginated list of properties
/// </summary>
public class PropertyListDto
{
    /// <summary>
    /// Collection of properties in the current page
    /// </summary>
    public List<PropertyDto> Items { get; set; } = new();
    
    /// <summary>
    /// Total number of properties across all pages
    /// </summary>
    public int TotalCount { get; set; }
    
    /// <summary>
    /// Current page number
    /// </summary>
    public int PageNumber { get; set; }
    
    /// <summary>
    /// Number of items per page
    /// </summary>
    public int PageSize { get; set; }
    
    /// <summary>
    /// Total number of pages
    /// </summary>
    public int TotalPages { get; set; }
}