import { render, screen, fireEvent } from '@testing-library/react';
import userEvent from '@testing-library/user-event';
import PropertyFilter from '../PropertyFilter';
import { PropertyFilter as PropertyFilterType } from '../../../models/Property';

describe('PropertyFilter Component', () => {
  // Create a properly typed mock function
  const mockFilterChange = jest.fn() as jest.MockedFunction<(filter: PropertyFilterType) => void>;

  beforeEach(() => {
    mockFilterChange.mockClear();
  });
  
  const renderComponent = (isLoading = false) => {
    return render(
      <PropertyFilter 
        onFilterChange={mockFilterChange}
        isLoading={isLoading}
      />
    );
  };
  
  test('renders all filter inputs correctly', () => {
    renderComponent();
    
    // Check if title is present
    expect(screen.getByText('Filter Properties')).toBeInTheDocument();
    
    // Check if all input fields are present
    expect(screen.getByLabelText('Property Name')).toBeInTheDocument();
    expect(screen.getByLabelText('Address')).toBeInTheDocument();
    expect(screen.getByLabelText('Min Price ($)')).toBeInTheDocument();
    expect(screen.getByLabelText('Max Price ($)')).toBeInTheDocument();
    
    // Check if buttons are present
    expect(screen.getByText('Apply Filters')).toBeInTheDocument();
    expect(screen.getByText('Reset')).toBeInTheDocument();
  });
  
  test('updates filter values when inputs change', async () => {
    renderComponent();
    
    // Get input fields
    const nameInput = screen.getByLabelText('Property Name');
    const addressInput = screen.getByLabelText('Address');
    const minPriceInput = screen.getByLabelText('Min Price ($)');
    const maxPriceInput = screen.getByLabelText('Max Price ($)');
    
    // Change input values
    await userEvent.type(nameInput, 'Test Property');
    await userEvent.type(addressInput, '123 Test St');
    await userEvent.type(minPriceInput, '100000');
    await userEvent.type(maxPriceInput, '500000');
    
    // Check if inputs have correct values
    expect(nameInput).toHaveValue('Test Property');
    expect(addressInput).toHaveValue('123 Test St');
    expect(minPriceInput).toHaveValue(100000);
    expect(maxPriceInput).toHaveValue(500000);
  });
  
  test('calls onFilterChange when Apply Filters button is clicked', async () => {
    renderComponent();
    
    // Fill out form
    await userEvent.type(screen.getByLabelText('Property Name'), 'Test');
    await userEvent.type(screen.getByLabelText('Min Price ($)'), '100000');
    
    // Click Apply Filters button
    fireEvent.click(screen.getByText('Apply Filters'));
    
    // Check if onFilterChange was called with correct values
    expect(mockFilterChange).toHaveBeenCalledTimes(1);
    expect(mockFilterChange).toHaveBeenCalledWith({
      name: 'Test',
      address: '',
      minPrice: 100000,
      maxPrice: undefined
    } as PropertyFilterType);
  });
  
  test('resets filter values when Reset button is clicked', async () => {
    renderComponent();
    
    // Fill out form
    const nameInput = screen.getByLabelText('Property Name');
    await userEvent.type(nameInput, 'Test Property');
    
    // Verify input has value
    expect(nameInput).toHaveValue('Test Property');
    
    // Click Reset button
    fireEvent.click(screen.getByText('Reset'));
    
    // Check if inputs are reset
    expect(nameInput).toHaveValue('');
    
    // Check if onFilterChange was called with reset values
    expect(mockFilterChange).toHaveBeenCalledTimes(1);
    expect(mockFilterChange).toHaveBeenCalledWith({
      name: '',
      address: '',
      minPrice: undefined,
      maxPrice: undefined
    } as PropertyFilterType);
  });
  
  test('disables inputs when isLoading is true', () => {
    renderComponent(true);
    
    // Check if all inputs are disabled
    expect(screen.getByLabelText('Property Name')).toBeDisabled();
    expect(screen.getByLabelText('Address')).toBeDisabled();
    expect(screen.getByLabelText('Min Price ($)')).toBeDisabled();
    expect(screen.getByLabelText('Max Price ($)')).toBeDisabled();
    
    // Check if buttons are disabled
    expect(screen.getByText('Apply Filters')).toBeDisabled();
    expect(screen.getByText('Reset')).toBeDisabled();
  });
});