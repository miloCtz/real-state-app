import { Link } from 'react-router-dom';
import { Property } from '../../models/Property';

// Type for the component props
interface PropertyCardProps {
  property: Property;
  getImageUrl: (imageName: string) => string;
  formatPrice: (price: number) => string;
}

/**
 * PropertyCard component that displays a single property card
 */
const PropertyCard = ({ property, getImageUrl, formatPrice }: PropertyCardProps) => {
  return (
    <div className="property-card">
      <div className="property-card-media">
        {property.images.length > 0 && property.images.some(img => img.enabled) ? (
          <img 
            src={getImageUrl(property.images[0].file)} 
            alt={property.name}
            className="property-image"
            loading="lazy"
            onError={(e) => {
              // If image fails to load, replace with placeholder
              const target = e.target as HTMLImageElement;
              target.onerror = null;
              target.src = '';
              target.alt = 'Image not available';
              target.parentElement?.classList.add('image-error');
            }}
          />
        ) : (
          <div className="property-image-placeholder">No Image Available</div>
        )}
      </div>
      <div className="property-card-content">
        <h3 className="property-card-title">{property.name}</h3>
        <div className="property-card-details">
          <p><span className="property-label">Address:</span> {property.address}</p>
          <p><span className="property-label">Price:</span> {formatPrice(property.price)}</p>
          <p><span className="property-label">Year Built:</span> {property.year}</p>
        </div>
        <div className="property-actions">
          <Link to={`/property/${property.id}`} className="details-button">
            DETAILS
          </Link>
        </div>
      </div>
    </div>
  );
};

export default PropertyCard;