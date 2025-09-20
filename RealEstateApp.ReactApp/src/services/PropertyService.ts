/**
 * API service for property-related endpoints
 */
import { Property, PropertyFilter, PropertyList } from '../models/Property';

/**
 * Base URL for API requests
 */
const API_BASE_URL = '/api';

/**
 * Handles HTTP requests to the Property API endpoints
 */
export const PropertyService = {
  /**
   * Fetches all properties from the API
   * @param pageNumber - Page number for pagination (default: 1)
   * @param pageSize - Number of items per page (default: 10)
   * @returns Promise containing property list data
   */
  getAllProperties: async (pageNumber = 1, pageSize = 10): Promise<PropertyList> => {
    try {
      const response = await fetch(`${API_BASE_URL}/properties?pageNumber=${pageNumber}&pageSize=${pageSize}`);
      
      if (!response.ok) {
        throw new Error(`Error fetching properties: ${response.status}`);
      }
      
      return await response.json() as PropertyList;
    } catch (error) {
      console.error('Failed to fetch properties:', error);
      throw error;
    }
  },

  /**
   * Fetches a single property by its ID
   * @param propertyId - ID of the property to fetch
   * @returns Promise containing property data
   */
  getPropertyById: async (propertyId: string): Promise<Property> => {
    try {
      const response = await fetch(`${API_BASE_URL}/properties/${propertyId}`);
      
      if (!response.ok) {
        throw new Error(`Error fetching property with ID ${propertyId}: ${response.status}`);
      }
      
      return await response.json() as Property;
    } catch (error) {
      console.error(`Failed to fetch property with ID ${propertyId}:`, error);
      throw error;
    }
  },

  /**
   * Searches properties based on filter criteria
   * @param filter - Filter criteria for properties
   * @returns Promise containing filtered property list data
   */
  searchProperties: async (filter: PropertyFilter): Promise<PropertyList> => {
    const queryParams = new URLSearchParams();
    
    // Add non-empty filter parameters to the query string
    Object.entries(filter).forEach(([key, value]) => {
      if (value !== undefined && value !== null && value !== '') {
        queryParams.append(key, String(value));
      }
    });
    
    try {
      const response = await fetch(`${API_BASE_URL}/properties?${queryParams.toString()}`);
      
      if (!response.ok) {
        throw new Error(`Error searching properties: ${response.status}`);
      }
      
      return await response.json() as PropertyList;
    } catch (error) {
      console.error('Failed to search properties:', error);
      throw error;
    }
  }
};

export default PropertyService;