import { useEffect, useState } from 'react';
import { PropertyService } from '../services/PropertyService';
import { PropertyList, PropertyFilter } from '../models/Property';
import PropertyGrid from '../components/Home/PropertyGrid';
import Pagination from '../components/Home/Pagination';
import PropertyFilterComponent from '../components/Home/PropertyFilter';

// Import all images from the assets/images folder
const imageContext = import.meta.glob('../assets/images/*.jpg', { eager: true });

/**
 * Home page component that displays a list of properties
 */
const Home = () => {
  const [propertyList, setPropertyList] = useState<PropertyList | null>(null);
  const [loading, setLoading] = useState<boolean>(true);
  const [error, setError] = useState<string | null>(null);
  const [currentPage, setCurrentPage] = useState<number>(1);
  const [filter, setFilter] = useState<PropertyFilter>({});
  const pageSize = 10;

  // Fetch properties when component mounts, page changes, or filter changes
  useEffect(() => {
    const fetchProperties = async () => {
      try {
        setLoading(true);
        
        // Apply filters and pagination
        const filterWithPagination: PropertyFilter = {
          ...filter,
          pageNumber: currentPage,
          pageSize: pageSize
        };
        
        // Use searchProperties instead of getAllProperties when filters are applied
        const data = await PropertyService.searchProperties(filterWithPagination);
        setPropertyList(data);
        setError(null);
      } catch (err) {
        setError('Failed to fetch properties. Please try again later.');
        console.error('Error fetching properties:', err);
      } finally {
        setLoading(false);
      }
    };

    fetchProperties();
  }, [currentPage, filter]);

  // Handle page change
  const handlePageChange = (newPage: number) => {
    setCurrentPage(newPage);
  };
  
  // Handle filter change
  const handleFilterChange = (newFilter: PropertyFilter) => {
    setFilter(newFilter);
    // Reset to first page when applying new filters
    setCurrentPage(1);
  };

  // Format currency for display
  const formatPrice = (price: number): string => {
    return new Intl.NumberFormat('en-US', {
      style: 'currency',
      currency: 'USD',
      minimumFractionDigits: 0,
    }).format(price);
  };

  // Function to get the image URL from the assets folder
  const getImageUrl = (imageName: string): string => {
    // Look for the image in the imported context
    const imageUrl = imageContext[`../assets/images/${imageName}`];
    
    // If the image exists in the context, return its URL
    if (imageUrl) {
      return (imageUrl as { default: string }).default;
    }
    
    // Default placeholder if image not found
    return '';
  };

  return (
    <div className="home-container">
      <h1>Available Properties</h1>
      
      {/* Property Filter Component */}
      <PropertyFilterComponent 
        onFilterChange={handleFilterChange}
        isLoading={loading}
      />

      {loading && <p className="loading-message">Loading properties...</p>}
      
      {error && <p className="error-message">{error}</p>}
      
      {!loading && !error && propertyList && propertyList.items.length === 0 && (
        <p className="no-properties-message">No properties found.</p>
      )}
      
      {!loading && !error && propertyList && propertyList.items.length > 0 && (
        <>
          <PropertyGrid 
            properties={propertyList.items} 
            getImageUrl={getImageUrl}
            formatPrice={formatPrice}
          />
          <Pagination 
            currentPage={currentPage}
            totalPages={propertyList.totalPages}
            onPageChange={handlePageChange}
          />
        </>
      )}
    </div>
  );
};

export default Home;