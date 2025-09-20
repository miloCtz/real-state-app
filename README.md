# Real Estate App

A modern real estate application built with .NET 9 Aspire, Blazor, and React.

## Prerequisites

Before running this application, make sure you have the following prerequisites installed:

- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0) - Required to run .NET 9 applications
- [Node.js](https://nodejs.org/) (v18 or later) - Required for the React frontend
- [Docker](https://www.docker.com/get-started) - Required for containerized services
- Visual Studio 2025 or Visual Studio Code with the following extensions:
  - C# Dev Kit
  - .NET Aspire SDK Components

## Project Structure

- `RealEstateApp.ApiService/` - Backend API service
- `RealEstateApp.ApiService.Tests/` - API service unit and integration tests
- `RealEstateApp.AppHost/` - Aspire Host Project (orchestrates all services)
- `RealEstateApp.Domain/` - Domain models, entities and business logic
- `RealEstateApp.Infrastructure/` - Cross-cutting concerns and infrastructure services
- `RealEstateApp.Persistence/` - Data access and repository implementations
- `RealEstateApp.ReactApp/` - React Frontend
- `RealEstateApp.ServiceDefaults/` - Shared service configurations and defaults

## Getting Started

1. Clone the repository:
```bash
git clone [repository-url]
cd real-state-app
```

2. Install React dependencies:
```bash
cd RealEstateApp.ReactApp
npm install
cd ..
```

3. Run the Aspire application:

There are two ways to run the application:

### Using Visual Studio:
- Open `RealEstateApp.sln`
- Set `RealEstateApp.AppHost` as the startup project
- Press F5 or click the "Run" button

### Using Command Line:
```bash
# Build the solution
dotnet build

# Run the Aspire orchestrator
dotnet run --project RealEstateApp.AppHost/RealEstateApp.AppHost.csproj

# Run tests
dotnet test
```

## Accessing the Application

Once the application is running:

- Aspire Dashboard: https://localhost:17266
- Login using the token provided in the console output
- Web Frontend (Blazor): http://localhost:[port] (port shown in Aspire Dashboard)
- API Service: http://localhost:[port] (port shown in Aspire Dashboard)
- React Frontend: http://localhost:[port] (port shown in Aspire Dashboard)

## Development Notes

- The Aspire Dashboard provides real-time monitoring of all services
- Each service can be debugged independently
- Configuration changes can be made through the respective `appsettings.json` files
- Environment-specific settings should be placed in `appsettings.Development.json`

## Technologies

- .NET 9 with Aspire 9.4.1
- React with Vite
- Azure Service Dependencies (if applicable)

## Common Issues

1. If you encounter port conflicts, they can be modified in:
   - `Properties/launchSettings.json` for each project
   - Aspire Dashboard will automatically assign new ports if defaults are in use

2. For Aspire-specific issues:
   - Ensure all required SDKs are installed
   - Check the Aspire Dashboard for detailed service logs
   - Verify all services are properly registered in `AppHost/Program.cs`

## Contributing

Please read [CONTRIBUTING.md](CONTRIBUTING.md) for details on our code of conduct and the process for submitting pull requests.

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details