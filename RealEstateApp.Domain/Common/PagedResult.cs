using System.Collections.Generic;

namespace RealEstateApp.Domain.Common;

/// <summary>
/// Represents a paginated result set with metadata
/// </summary>
/// <typeparam name="T">The type of items in the result set</typeparam>
public class PagedResult<T>
{
    /// <summary>
    /// Collection of items in the current page
    /// </summary>
    public IEnumerable<T> Items { get; set; } = [];
    
    /// <summary>
    /// Total number of items across all pages
    /// </summary>
    public int TotalCount { get; set; }
    
    /// <summary>
    /// Current page number (1-based)
    /// </summary>
    public int PageNumber { get; set; }
    
    /// <summary>
    /// Number of items per page
    /// </summary>
    public int PageSize { get; set; }
    
    /// <summary>
    /// Total number of pages
    /// </summary>
    public int TotalPages => (TotalCount + PageSize - 1) / PageSize;
}

/// <summary>
/// Filter criteria for property searches
/// </summary>
public class PropertyFilter
{
    /// <summary>
    /// Filter by property name (partial match)
    /// </summary>
    public string? Name { get; set; }
    
    /// <summary>
    /// Filter by property address (partial match)
    /// </summary>
    public string? Address { get; set; }
    
    /// <summary>
    /// Filter by minimum price (inclusive)
    /// </summary>
    public decimal? MinPrice { get; set; }
    
    /// <summary>
    /// Filter by maximum price (inclusive)
    /// </summary>
    public decimal? MaxPrice { get; set; }
    
    /// <summary>
    /// Page number for pagination (1-based)
    /// </summary>
    public int PageNumber { get; set; } = 1;
    
    /// <summary>
    /// Number of items per page
    /// </summary>
    public int PageSize { get; set; } = 10;
}