import { useEffect, useState } from 'react';
import { useParams, Link, useNavigate } from 'react-router-dom';
import { PropertyService } from '../services/PropertyService';
import { Property } from '../models/Property';

/**
 * PropertyDetail page component that displays detailed information about a property
 */
const PropertyDetail = () => {
  const { id } = useParams<{ id: string }>();
  const navigate = useNavigate();
  const [property, setProperty] = useState<Property | null>(null);
  const [loading, setLoading] = useState<boolean>(true);
  const [error, setError] = useState<string | null>(null);

  // Fetch property details when component mounts
  useEffect(() => {
    const fetchPropertyDetails = async () => {
      if (!id) {
        setError('Property ID is missing.');
        setLoading(false);
        return;
      }

      try {
        setLoading(true);
        const data = await PropertyService.getPropertyById(id);
        setProperty(data);
        setError(null);
      } catch (err) {
        setError('Failed to fetch property details. Please try again later.');
        console.error('Error fetching property details:', err);
      } finally {
        setLoading(false);
      }
    };

    fetchPropertyDetails();
  }, [id]);

  // Format currency for display
  const formatPrice = (price: number): string => {
    return new Intl.NumberFormat('en-US', {
      style: 'currency',
      currency: 'USD',
      minimumFractionDigits: 0,
    }).format(price);
  };

  // Format date for display
  const formatDate = (dateString: string): string => {
    const date = new Date(dateString);
    return date.toLocaleDateString('en-US', {
      year: 'numeric',
      month: 'long',
      day: 'numeric',
    });
  };

  // Handle back button click
  const handleBack = () => {
    navigate(-1);
  };

  return (
    <div className="property-detail-container">
      <button onClick={handleBack} className="back-button">
        &larr; Back to Properties
      </button>

      {loading && <p className="loading-message">Loading property details...</p>}
      
      {error && <p className="error-message">{error}</p>}
      
      {!loading && !error && !property && (
        <div className="property-not-found">
          <h2>Property Not Found</h2>
          <p>The property you're looking for doesn't exist or has been removed.</p>
          <Link to="/" className="home-link">Return to Home</Link>
        </div>
      )}
      
      {!loading && !error && property && (
        <div className="property-detail">
          <h1>{property.name}</h1>
          
          <div className="property-images">
            {property.images.filter(img => img.enabled).length > 0 ? (
              property.images
                .filter(img => img.enabled)
                .map((image) => (
                  <img 
                    key={image.id} 
                    src={image.file} 
                    alt={`${property.name} - Image ${image.id}`}
                    className="property-detail-image"
                  />
                ))
            ) : (
              <div className="no-image">No images available</div>
            )}
          </div>
          
          <div className="property-info">
            <div className="info-section">
              <h2>Property Details</h2>
              <p><strong>Price:</strong> {formatPrice(property.price)}</p>
              <p><strong>Address:</strong> {property.address}</p>
              <p><strong>Year Built:</strong> {property.year}</p>
              <p><strong>Internal Code:</strong> {property.codeInternal}</p>
            </div>
            
            {property.owner && (
              <div className="info-section">
                <h2>Owner Information</h2>
                {property.owner.photo && (
                  <img 
                    src={property.owner.photo} 
                    alt={property.owner.name}
                    className="owner-photo"
                  />
                )}
                <p><strong>Name:</strong> {property.owner.name}</p>
                <p><strong>Address:</strong> {property.owner.address}</p>
                <p><strong>Birthday:</strong> {formatDate(property.owner.birthday)}</p>
              </div>
            )}
          </div>
        </div>
      )}
    </div>
  );
};

export default PropertyDetail;