import { Property } from '../../models/Property';

// Type for the component props
interface PropertyInfoProps {
  property: Property;
  formatPrice: (price: number) => string;
  formatDate: (dateString: string) => string;
  getImageUrl: (imageName: string) => string;
}

/**
 * PropertyInfo component that displays detailed property information
 */
const PropertyInfo = ({ property, formatPrice, formatDate, getImageUrl }: PropertyInfoProps) => {
  return (
    <div className="property-info">
      <div className="info-section">
        <h2>Property Details</h2>
        <div className="property-details-grid">
          <div className="detail-item">
            <span className="detail-label">Price:</span>
            <span className="detail-value">{formatPrice(property.price)}</span>
          </div>
          <div className="detail-item">
            <span className="detail-label">Address:</span>
            <span className="detail-value">{property.address}</span>
          </div>
          <div className="detail-item">
            <span className="detail-label">Year Built:</span>
            <span className="detail-value">{property.year}</span>
          </div>
          <div className="detail-item">
            <span className="detail-label">Internal Code:</span>
            <span className="detail-value">{property.codeInternal}</span>
          </div>
        </div>
      </div>
      
      {property.owner && (
        <div className="info-section">
          <h2>Owner Information</h2>
          <div className="owner-info">
            {property.owner.photo && (
              <div className="owner-photo-container">
                <img 
                  src={getImageUrl(property.owner.photo)} 
                  alt={property.owner.name}
                  className="owner-photo"
                  loading="lazy"
                  onError={(e) => {
                    // If owner photo fails to load
                    const target = e.target as HTMLImageElement;
                    target.onerror = null;
                    target.src = '';
                    target.alt = 'Photo not available';
                    target.classList.add('owner-photo-error');
                  }}
                />
              </div>
            )}
            <div className="owner-details">
              <div className="detail-item">
                <span className="detail-label">Name:</span>
                <span className="detail-value">{property.owner.name}</span>
              </div>
              <div className="detail-item">
                <span className="detail-label">Address:</span>
                <span className="detail-value">{property.owner.address}</span>
              </div>
              <div className="detail-item">
                <span className="detail-label">Birthday:</span>
                <span className="detail-value">{formatDate(property.owner.birthday)}</span>
              </div>
            </div>
          </div>
        </div>
      )}
    </div>
  );
};

export default PropertyInfo;