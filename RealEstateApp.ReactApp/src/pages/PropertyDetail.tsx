import { useEffect, useState } from 'react';
import { useParams, Link, useNavigate } from 'react-router-dom';
import { PropertyService } from '../services/PropertyService';
import { Property } from '../models/Property';
import PropertyImages from '../components/PropertyDetail/PropertyImages';
import PropertyInfo from '../components/PropertyDetail/PropertyInfo';
import PropertyTraces from '../components/PropertyDetail/PropertyTraces';

// Import all images from the assets/images folder
const imageContext = import.meta.glob('../assets/images/*.jpg', { eager: true });

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
          <h1 className="property-title">{property.name}</h1>
          
          <PropertyImages 
            propertyName={property.name}
            images={property.images}
            getImageUrl={getImageUrl}
          />
          
          <PropertyInfo 
            property={property}
            formatPrice={formatPrice}
            formatDate={formatDate}
            getImageUrl={getImageUrl}
          />
          
          <PropertyTraces
            traces={property.traces}
            formatPrice={formatPrice}
            formatDate={formatDate}
          />
        </div>
      )}
    </div>
  );
};

export default PropertyDetail;