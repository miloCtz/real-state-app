import React, { useState, FormEvent } from 'react';
import { PropertyFilter as PropertyFilterType } from '../../models/Property';

interface PropertyFilterProps {
  onFilterChange: (filter: PropertyFilterType) => void;
  isLoading: boolean;
}

const PropertyFilter: React.FC<PropertyFilterProps> = ({ onFilterChange, isLoading }) => {
  const [filter, setFilter] = useState<PropertyFilterType>({
    name: '',
    address: '',
    minPrice: undefined,
    maxPrice: undefined
  });

  const handleInputChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const { name, value } = e.target;
    
    // Handle number inputs
    if (name === 'minPrice' || name === 'maxPrice') {
      const numberValue = value === '' ? undefined : Number(value);
      setFilter(prev => ({
        ...prev,
        [name]: numberValue
      }));
    } else {
      setFilter(prev => ({
        ...prev,
        [name]: value
      }));
    }
  };

  const handleSubmit = (e: FormEvent) => {
    e.preventDefault();
    onFilterChange(filter);
  };

  const handleReset = () => {
    const resetFilter: PropertyFilterType = {
      name: '',
      address: '',
      minPrice: undefined,
      maxPrice: undefined
    };
    
    setFilter(resetFilter);
    onFilterChange(resetFilter);
  };

  return (
    <div className="property-filter-container">
      <h2>Filter Properties</h2>
      <form onSubmit={handleSubmit} className="property-filter-form">
        <div className="filter-row">
          <div className="filter-group">
            <label htmlFor="name">Property Name</label>
            <input
              type="text"
              id="name"
              name="name"
              value={filter.name || ''}
              onChange={handleInputChange}
              placeholder="Enter property name"
              disabled={isLoading}
            />
          </div>
          
          <div className="filter-group">
            <label htmlFor="address">Address</label>
            <input
              type="text"
              id="address"
              name="address"
              value={filter.address || ''}
              onChange={handleInputChange}
              placeholder="Enter address"
              disabled={isLoading}
            />
          </div>
        </div>
        
        <div className="filter-row">
          <div className="filter-group">
            <label htmlFor="minPrice">Min Price ($)</label>
            <input
              type="number"
              id="minPrice"
              name="minPrice"
              value={filter.minPrice || ''}
              onChange={handleInputChange}
              placeholder="Min price"
              min="0"
              disabled={isLoading}
            />
          </div>
          
          <div className="filter-group">
            <label htmlFor="maxPrice">Max Price ($)</label>
            <input
              type="number"
              id="maxPrice"
              name="maxPrice"
              value={filter.maxPrice || ''}
              onChange={handleInputChange}
              placeholder="Max price"
              min="0"
              disabled={isLoading}
            />
          </div>
        </div>
        
        <div className="filter-actions">
          <button 
            type="submit" 
            className="filter-button filter-apply"
            disabled={isLoading}
          >
            Apply Filters
          </button>
          <button 
            type="button" 
            className="filter-button filter-reset"
            onClick={handleReset}
            disabled={isLoading}
          >
            Reset
          </button>
        </div>
      </form>
    </div>
  );
};

export default PropertyFilter;