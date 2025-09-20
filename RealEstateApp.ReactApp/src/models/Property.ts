/**
 * Represents a property owner
 */
export interface Owner {
  id: number;
  name: string;
  address: string;
  photo: string;
  birthday: string; // ISO date string
}

/**
 * Represents a property image
 */
export interface PropertyImage {
  id: number;
  file: string; // URL or file path
  enabled: boolean;
}

/**
 * Represents a real estate property
 */
export interface Property {
  id: string;
  name: string;
  address: string;
  price: number;
  codeInternal: string;
  year: number;
  idOwner: number;
  owner?: Owner;
  images: PropertyImage[];
}

/**
 * Represents a paginated list of properties
 */
export interface PropertyList {
  items: Property[];
  totalCount: number;
  pageNumber: number;
  pageSize: number;
  totalPages: number;
}

/**
 * Represents filter criteria for property searches
 */
export interface PropertyFilter {
  name?: string;
  address?: string;
  minPrice?: number;
  maxPrice?: number;
  pageNumber?: number;
  pageSize?: number;
}