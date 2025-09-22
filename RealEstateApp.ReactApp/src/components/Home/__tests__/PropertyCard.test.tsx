import { render, screen } from '@testing-library/react';
import { BrowserRouter } from 'react-router-dom';
import PropertyCard from '../PropertyCard';
import { Property } from '../../../models/Property';

// Mock property data
const mockProperty: Property = {
  id: '1',
  name: 'Test Property',
  address: '123 Test Street',
  price: 250000,
  codeInternal: 'TP001',
  year: 2020,
  idOwner: 1,
  images: [
    { id: 1, file: 'property_1.jpg', enabled: true }
  ]
};

// Mock functions with proper typing
const mockGetImageUrl = jest.fn((imageName: string) => `/assets/images/${imageName}`) as jest.MockedFunction<(imageName: string) => string>;
const mockFormatPrice = jest.fn((price: number) => `$${price.toLocaleString()}`) as jest.MockedFunction<(price: number) => string>;




const renderPropertyCard = (property: Property = mockProperty) => {
  return render(
    <BrowserRouter>
      <PropertyCard
        property={property}
        getImageUrl={mockGetImageUrl}
        formatPrice={mockFormatPrice}
      />
    </BrowserRouter>
  );
};

describe('PropertyCard Component', () => {
  // Reset mocks before each test
  beforeEach(() => {
    mockGetImageUrl.mockClear();
    mockFormatPrice.mockClear();
  });
  
  test('renders property details correctly', () => {
    renderPropertyCard();
    
    // Check if property name is displayed
    expect(screen.getByText('Test Property')).toBeInTheDocument();
    
    // Check if address is displayed
    expect(screen.getByText('Address:')).toBeInTheDocument();
    expect(screen.getByText('123 Test Street', { exact: false })).toBeInTheDocument();
    
    // Check if price is displayed
    expect(screen.getByText('Price:')).toBeInTheDocument();
    expect(mockFormatPrice).toHaveBeenCalledWith(250000);
    
    // Check if year built is displayed
    expect(screen.getByText('Year Built:')).toBeInTheDocument();
    expect(screen.getByText('2020', { exact: false })).toBeInTheDocument();
    
    // Check if details link is present
    const detailsLink = screen.getByText('DETAILS');
    expect(detailsLink).toBeInTheDocument();
    expect(detailsLink.closest('a')).toHaveAttribute('href', '/property/1');
  });
  
  test('renders image when available', () => {
    renderPropertyCard();
    
    // Check if image is displayed
    const image = screen.getByRole('img', { name: 'Test Property' });
    expect(image).toBeInTheDocument();
    expect(mockGetImageUrl).toHaveBeenCalledWith('property_1.jpg');
    expect(image).toHaveAttribute('src', '/assets/images/property_1.jpg');
  });
  
  test('displays placeholder when no image is available', () => {
    const propertyWithNoImage: Property = {
      ...mockProperty,
      images: []
    };
    
    renderPropertyCard(propertyWithNoImage);
    
    // Check if placeholder is displayed
    expect(screen.getByText('No Image Available')).toBeInTheDocument();
    // Verify mockGetImageUrl was not called since there's no image
    expect(mockGetImageUrl).not.toHaveBeenCalledWith('property_1.jpg');
  });
  
  test('displays placeholder when image is disabled', () => {
    const propertyWithDisabledImage: Property = {
      ...mockProperty,
      images: [{ id: 1, file: 'property_1.jpg', enabled: false }]
    };
    
    renderPropertyCard(propertyWithDisabledImage);
    
    // Check if placeholder is displayed
    expect(screen.getByText('No Image Available')).toBeInTheDocument();
    // Verify that we don't use the disabled image
    expect(mockGetImageUrl).not.toHaveBeenCalledWith('property_1.jpg');
  });
});