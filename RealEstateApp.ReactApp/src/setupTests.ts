// Import required testing libraries
import '@testing-library/jest-dom';
import 'whatwg-fetch';

// Mock the matchMedia function
Object.defineProperty(window, 'matchMedia', {
  writable: true,
  value: jest.fn().mockImplementation(query => ({
    matches: false,
    media: query,
    onchange: null,
    addListener: jest.fn(), // deprecated
    removeListener: jest.fn(), // deprecated
    addEventListener: jest.fn(),
    removeEventListener: jest.fn(),
    dispatchEvent: jest.fn(),
  })),
});

// Create a global mock for import.meta.glob
const mockImageModules = {
  '../assets/images/property_1.jpg': { default: '/assets/images/property_1.jpg' },
  '../assets/images/property_2.jpg': { default: '/assets/images/property_2.jpg' },
  '../assets/images/property_3.jpg': { default: '/assets/images/property_3.jpg' },
};

// Mock fetch API
global.fetch = jest.fn() as jest.MockedFunction<
  (input: RequestInfo | URL, init?: RequestInit) => Promise<Response>
>;

// Setup a global import.meta mock for Vite
(globalThis as any).import = {
  meta: {
    glob: (_path: string) => mockImageModules
  }
} as any;