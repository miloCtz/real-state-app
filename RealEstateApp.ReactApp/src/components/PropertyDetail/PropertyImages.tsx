import { PropertyImage } from '../../models/Property';

// Type for the component props
interface PropertyImagesProps {
  propertyName: string;
  images: PropertyImage[];
  getImageUrl: (imageName: string) => string;
}

/**
 * PropertyImages component that displays a grid of property images
 */
const PropertyImages = ({ propertyName, images, getImageUrl }: PropertyImagesProps) => {
  const enabledImages = images.filter(img => img.enabled);

  return (
    <div className="property-images">
      {enabledImages.length > 0 ? (
        enabledImages.map((image, index) => (
          <img 
            key={`image-${image.id || index}`} 
            src={getImageUrl(image.file)} 
            alt={`${propertyName} - Image ${index + 1}`}
            className="property-detail-image"
            loading="lazy"
            onError={(e) => {
              // If image fails to load, replace with placeholder
              const target = e.target as HTMLImageElement;
              target.onerror = null;
              target.style.display = 'none';
              const container = document.createElement('div');
              container.className = 'no-image';
              container.textContent = 'Image not available';
              target.parentElement?.appendChild(container);
            }}
          />
        ))
      ) : (
        <div className="no-image">No images available</div>
      )}
    </div>
  );
};

export default PropertyImages;