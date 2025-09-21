import { Property } from '../../models/Property';
import PropertyCard from './PropertyCard';

// Type for the component props
interface PropertyGridProps {
  properties: Property[];
  getImageUrl: (imageName: string) => string;
  formatPrice: (price: number) => string;
}

/**
 * PropertyGrid component that displays a grid of property cards
 */
const PropertyGrid = ({ properties, getImageUrl, formatPrice }: PropertyGridProps) => {
  return (
    <div className="property-grid">
      {properties.map((property) => (
        <PropertyCard 
          key={property.id}
          property={property}
          getImageUrl={getImageUrl}
          formatPrice={formatPrice}
        />
      ))}
    </div>
  );
};

export default PropertyGrid;