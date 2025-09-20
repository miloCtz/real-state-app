# RealEstateApp.ApiService.Tests

This project contains unit tests for the RealEstateApp.ApiService, focused on testing the API functionality including endpoints, mappings, and repositories.

## Test Classes

### PropertyEndpointsTests
Tests the property endpoints in the API, specifically:
- `GET /api/properties` - Retrieving a paginated list of properties with filtering
- `GET /api/properties/{id}` - Retrieving a specific property by ID

### MappingTests
Tests the Mapster mappings used to convert between domain entities and DTOs:
- Property to PropertyDto mapping
- PagedResult<Property> to PropertyListDto mapping

### PropertyRepositoryTests
Tests the repository functionality for accessing property data:
- Getting a property by ID
- Getting a paginated list of properties with filtering

### TestDataTests
Tests the test data generation utilities to ensure they create valid test objects.

## Running the Tests

To run all tests:

```bash
dotnet test
```

To run a specific test class:

```bash
dotnet test --filter "FullyQualifiedName~RealEstateApp.ApiService.Tests.Endpoints.PropertyEndpointsTests"
```

To run a specific test:

```bash
dotnet test --filter "Name=GetProperties_ReturnsOkResultWithPagedList"
```

## Integration Tests (Commented Out)

The project includes commented out integration tests in `Integration/PropertyIntegrationTests.cs` that demonstrate how to set up tests with a real MongoDB instance. These tests are not active by default as they require a running MongoDB database.

To run the integration tests:
1. Ensure MongoDB is running locally
2. Uncomment the integration test class constructor and `[Collection]` attribute
3. Run the tests specifically targeting the integration tests

## Mock vs. Real Dependencies

- Unit tests use Moq to mock dependencies
- Integration tests (when enabled) connect to a real MongoDB instance

## Test Data

Test data generators are provided in `TestData.cs` to create consistent test objects.