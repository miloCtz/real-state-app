import { PropertyTrace } from '../../models/Property';

// Type for the component props
interface PropertyTracesProps {
  traces?: PropertyTrace[];
  formatPrice: (price: number) => string;
  formatDate: (dateString: string) => string;
}

/**
 * PropertyTraces component that displays property history/activity
 */
const PropertyTraces = ({ traces, formatPrice, formatDate }: PropertyTracesProps) => {
  // Return null if there are no traces
  if (!traces || traces.length === 0) {
    return null;
  }

  return (
    <div className="info-section">
      <h2>Property History</h2>
      <div className="property-traces">
        <table className="traces-table">
          <thead>
            <tr>
              <th>Date</th>
              <th>Description</th>
              <th>Value</th>
              <th>Tax</th>
            </tr>
          </thead>
          <tbody>
            {traces.map((trace) => (
              <tr key={trace.id}>
                <td>{formatDate(trace.dateCreated)}</td>
                <td>{trace.name}</td>
                <td>{formatPrice(trace.value)}</td>
                <td>{formatPrice(trace.tax)}</td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
    </div>
  );
};

export default PropertyTraces;