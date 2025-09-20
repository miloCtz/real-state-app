import { useEffect, useState } from 'react';
import { Link } from 'react-router-dom';
import { PropertyService } from '../services/PropertyService';
import { PropertyList, Property } from '../models/Property';

/**
 * Home page component that displays a list of properties
 */
const Home = () => {
  const [propertyList, setPropertyList] = useState<PropertyList | null>(null);
  const [loading, setLoading] = useState<boolean>(true);
  const [error, setError] = useState<string | null>(null);
  const [currentPage, setCurrentPage] = useState<number>(1);
  const pageSize = 10;

  // Fetch properties when component mounts or page changes
  useEffect(() => {
    const fetchProperties = async () => {
      try {
        setLoading(true);
        const data = await PropertyService.getAllProperties(currentPage, pageSize);
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
  }, [currentPage]);

  // Handle page change
  const handlePageChange = (newPage: number) => {
    setCurrentPage(newPage);
  };

  // Format currency for display
  const formatPrice = (price: number): string => {
    return new Intl.NumberFormat('en-US', {
      style: 'currency',
      currency: 'USD',
      minimumFractionDigits: 0,
    }).format(price);
  };

  // Render property cards
  const renderPropertyCards = (properties: Property[]) => {
    return properties.map((property) => (
      <div key={property.id} className="property-card">
        <h3>{property.name}</h3>
        <p>Address: {property.address}</p>
        <p>Price: {formatPrice(property.price)}</p>
        <p>Year Built: {property.year}</p>
        {property.images.length > 0 && property.images.some(img => img.enabled) && (
          <img 
            src={property.images.find(img => img.enabled)?.file || property.images[0].file} 
            alt={property.name}
            className="property-image"
          />
        )}
        <div className="property-actions">
          <Link to={`/property/${property.id}`} className="details-button">
            DETAILS
          </Link>
        </div>
      </div>
    ));
  };

  // Render pagination controls
  const renderPagination = () => {
    if (!propertyList || propertyList.totalPages <= 1) return null;

    return (
      <div className="pagination">
        <button 
          onClick={() => handlePageChange(currentPage - 1)} 
          disabled={currentPage === 1}
          className="pagination-button"
        >
          Previous
        </button>
        <span className="page-info">
          Page {currentPage} of {propertyList.totalPages}
        </span>
        <button 
          onClick={() => handlePageChange(currentPage + 1)} 
          disabled={currentPage === propertyList.totalPages}
          className="pagination-button"
        >
          Next
        </button>
      </div>
    );
  };

  return (
    <div className="home-container">
      <h1>Available Properties</h1>

      {loading && <p className="loading-message">Loading properties...</p>}
      
      {error && <p className="error-message">{error}</p>}
      
      {!loading && !error && propertyList && propertyList.items.length === 0 && (
        <p className="no-properties-message">No properties found.</p>
      )}
      
      {!loading && !error && propertyList && propertyList.items.length > 0 && (
        <>
          <div className="property-grid">
            {renderPropertyCards(propertyList.items)}
          </div>
          {renderPagination()}
        </>
      )}
    </div>
  );
};

export default Home;