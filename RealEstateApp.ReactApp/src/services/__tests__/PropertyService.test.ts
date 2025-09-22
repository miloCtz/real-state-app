/* eslint-disable */
import PropertyService from '../PropertyService';

// Mock fetch API
global.fetch = jest.fn() as unknown as typeof fetch;

// Mock console.error to avoid cluttering test output
jest.spyOn(console, 'error').mockImplementation(() => {});

// Helper function to create a mock response
const createMockResponse = (data: any, status = 200) => {
  return {
    ok: status >= 200 && status < 300,
    status,
    json: jest.fn().mockResolvedValue(data)
  };
};

describe('PropertyService', () => {
  // Clear all mocks before each test
  beforeEach(() => {
    jest.clearAllMocks();
  });
  
  describe('getAllProperties', () => {
    test('fetches properties successfully', async () => {
      // Mock data to be returned
      const mockData = {
        items: [
          { id: '1', name: 'Property 1', price: 100000 },
          { id: '2', name: 'Property 2', price: 200000 }
        ],
        totalCount: 2,
        pageNumber: 1,
        pageSize: 10,
        totalPages: 1
      };
      
      // Set up the fetch mock to return a successful response
      (fetch as jest.Mock).mockResolvedValueOnce(createMockResponse(mockData));
      
      // Call the function
      const result = await PropertyService.getAllProperties(1, 10);
      
      // Assertions
      expect(fetch).toHaveBeenCalledWith('/api/api/properties?pageNumber=1&pageSize=10');
      expect(result).toEqual(mockData);
    });
    
    test('handles API errors appropriately', async () => {
      // Set up the fetch mock to return an error response
      (fetch as jest.Mock).mockResolvedValueOnce(createMockResponse({}, 500));
      
      // Expect the function to throw an error
      await expect(PropertyService.getAllProperties(1, 10)).rejects.toThrow('Error fetching properties: 500');
      expect(console.error).toHaveBeenCalled();
    });
    
    test('uses default pagination values when not provided', async () => {
      // Mock data to be returned
      const mockData = {
        items: [],
        totalCount: 0,
        pageNumber: 1,
        pageSize: 10,
        totalPages: 0
      };
      
      // Set up the fetch mock to return a successful response
      (fetch as jest.Mock).mockResolvedValueOnce(createMockResponse(mockData));
      
      // Call the function without parameters
      await PropertyService.getAllProperties();
      
      // Assertions
      expect(fetch).toHaveBeenCalledWith('/api/api/properties?pageNumber=1&pageSize=10');
    });
  });
  
  describe('getPropertyById', () => {
    test('fetches a single property by ID successfully', async () => {
      // Mock data to be returned
      const mockProperty = {
        id: '123',
        name: 'Test Property',
        price: 150000
      };
      
      // Set up the fetch mock to return a successful response
      (fetch as jest.Mock).mockResolvedValueOnce(createMockResponse(mockProperty));
      
      // Call the function
      const result = await PropertyService.getPropertyById('123');
      
      // Assertions
      expect(fetch).toHaveBeenCalledWith('/api/api/properties/123');
      expect(result).toEqual(mockProperty);
    });
    
    test('handles API errors when fetching single property', async () => {
      // Set up the fetch mock to return an error response
      (fetch as jest.Mock).mockResolvedValueOnce(createMockResponse({}, 404));
      
      // Expect the function to throw an error
      await expect(PropertyService.getPropertyById('999')).rejects.toThrow('Error fetching property with ID 999: 404');
      expect(console.error).toHaveBeenCalled();
    });
  });
  
  describe('searchProperties', () => {
    test('searches properties with multiple filter parameters', async () => {
      // Mock data to be returned
      const mockData = {
        items: [{ id: '1', name: 'Luxury Villa', price: 350000 }],
        totalCount: 1,
        pageNumber: 1,
        pageSize: 10,
        totalPages: 1
      };
      
      // Filter to apply
      const filter = {
        name: 'Luxury',
        minPrice: 300000,
        maxPrice: 400000,
        pageNumber: 1,
        pageSize: 10
      };
      
      // Set up the fetch mock to return a successful response
      (fetch as jest.Mock).mockResolvedValueOnce(createMockResponse(mockData));
      
      // Call the function
      const result = await PropertyService.searchProperties(filter);
      
      // Assertions
      expect(fetch).toHaveBeenCalledWith(expect.stringMatching(/\/api\/api\/properties\?.+/));
      const fetchUrl = (fetch as jest.Mock).mock.calls[0][0];
      expect(fetchUrl).toContain('name=Luxury');
      expect(fetchUrl).toContain('minPrice=300000');
      expect(fetchUrl).toContain('maxPrice=400000');
      expect(fetchUrl).toContain('pageNumber=1');
      expect(fetchUrl).toContain('pageSize=10');
      expect(result).toEqual(mockData);
    });
    
    test('searches properties with empty filter', async () => {
      // Mock data to be returned
      const mockData = {
        items: [
          { id: '1', name: 'Property 1', price: 100000 },
          { id: '2', name: 'Property 2', price: 200000 }
        ],
        totalCount: 2,
        pageNumber: 1,
        pageSize: 10,
        totalPages: 1
      };
      
      // Empty filter
      const filter = {};
      
      // Set up the fetch mock to return a successful response
      (fetch as jest.Mock).mockResolvedValueOnce(createMockResponse(mockData));
      
      // Call the function
      const result = await PropertyService.searchProperties(filter);
      
      // Assertions
      expect(fetch).toHaveBeenCalledWith('/api/api/properties?');
      expect(result).toEqual(mockData);
    });
    
    test('handles API errors when searching properties', async () => {
      // Set up the fetch mock to return an error response
      (fetch as jest.Mock).mockResolvedValueOnce(createMockResponse({}, 500));
      
      // Expect the function to throw an error
      await expect(PropertyService.searchProperties({ name: 'Test' })).rejects.toThrow('Error searching properties: 500');
      expect(console.error).toHaveBeenCalled();
    });
    
    test('skips undefined, null or empty string parameters', async () => {
      // Mock data to be returned
      const mockData = { items: [], totalCount: 0, pageNumber: 1, pageSize: 10, totalPages: 0 };
      
      // Filter with undefined, null, and empty values
      const filter = {
        name: 'Test',
        address: '',
        minPrice: undefined,
        maxPrice: undefined
      };
      
      // Set up the fetch mock to return a successful response
      (fetch as jest.Mock).mockResolvedValueOnce(createMockResponse(mockData));
      
      // Call the function
      await PropertyService.searchProperties(filter);
      
      // Assertions
      expect(fetch).toHaveBeenCalledWith('/api/api/properties?name=Test');
    });
  });
});