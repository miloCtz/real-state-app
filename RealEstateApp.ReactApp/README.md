# Real Estate Application

A modern React application for browsing and filtering real estate property listings.

## Features

- Browse properties with pagination
- Filter properties by name, address, and price range
- View detailed property information
- Responsive design for mobile and desktop
- Dark mode support

## Technologies Used

- React 19 with TypeScript
- React Router for navigation
- Vite as build tool and dev server
- SMACSS methodology for CSS organization
- Jest and React Testing Library for testing

## Getting Started with Development

This project was bootstrapped with [Vite](https://vitejs.dev/guide/).

### Prerequisites

- Node.js (v18 or newer)
- npm or yarn

### Available Scripts

In the project directory, you can run:

#### `npm install`

Installs the dependencies required to run the app.

#### `npm start`

Runs the app in the development mode.\
Open [http://localhost:5173/](http://localhost:5173/) to view it in your browser.

The page will reload when you make changes.\
You may also see any lint errors in the console.

#### `npm run build`

Builds the app for production to the `dist` folder.\
It correctly bundles React in [production mode](https://vitejs.dev/guide/build.html) and optimizes the build for the best performance.

#### `npm test`

Launches the test runner in interactive watch mode.\
Tests are written using Jest and React Testing Library.

For running specific tests:
```bash
npm test -- src/components/Home/__tests__/PropertyCard.test.tsx
```

To see test coverage:
```bash
npm test -- --coverage
```

### Testing

The project uses Jest and React Testing Library for unit and component testing. The test files are located next to the components they test in `__tests__` folders.

Current test coverage:
- Statements: 31.73%
- Branches: 36.11%
- Functions: 27.27%
- Lines: 31.28%

Key components with tests:
- PropertyFilter: 100% coverage
- PropertyCard: 50% coverage
- PropertyService: 100% coverage

Your app is ready to be deployed!

See the section about [deployment](https://vitejs.dev/guide/static-deploy.html) for more information.

### Testing

This project uses Jest and React Testing Library for unit and component testing. 

#### `npm test`

Runs all the tests in the project.

#### `npm test:watch`

Runs the tests in watch mode. This is useful during development as it will automatically re-run tests when files change.

#### `npm test:coverage`

Generates a test coverage report in the `coverage` directory.

## Testing Strategy

This project follows a comprehensive testing strategy to ensure code quality and reliability:

### Component Testing
- **Presentational Components**: Tests for correct rendering, props handling, and user interactions
- **Container Components**: Tests for state management, data fetching, and lifecycle behaviors
- **Form Components**: Tests for validation, submission, and error states

### Service Testing
- **API Services**: Tests for API calls, parameter handling, response processing, and error handling
- **Utility Functions**: Tests for correct input/output behavior and edge cases

### Test Structure
Each test suite follows the Arrange-Act-Assert pattern:
1. **Arrange**: Set up the test environment and inputs
2. **Act**: Perform the action being tested
3. **Assert**: Verify the expected outcome

### Test Coverage Requirements
The project aims for at least 70% code coverage across:
- Statements
- Branches
- Functions
- Lines

## Project Structure

```
src/
├── assets/          # Static assets (images, icons)
├── components/      # Reusable UI components
├── hooks/           # Custom React hooks
├── layouts/         # Page layouts
├── models/          # TypeScript interfaces and types
├── pages/           # Page components
├── routes/          # Routing configuration
├── services/        # API and data services
└── styles/          # CSS styles following SMACSS methodology
    ├── base/        # Base styles for HTML elements
    ├── layout/      # Layout styles for page structure
    ├── modules/     # Reusable module styles
    ├── state/       # State-based styles
    └── theme/       # Theme variables and settings
```

## Learn More

- [Vite documentation](https://vitejs.dev/)
- [React documentation](https://reactjs.org/)
- [TypeScript documentation](https://www.typescriptlang.org/docs/)
- [Jest documentation](https://jestjs.io/docs/getting-started)
- [React Testing Library documentation](https://testing-library.com/docs/react-testing-library/intro/)

### Troubleshooting

If you encounter problems:
- Check the [Vite troubleshooting guide](https://vitejs.dev/guide/troubleshooting.html)
- Ensure all dependencies are installed correctly with `npm install`
- Check for TypeScript errors with `npx tsc --noEmit`
- Clear browser cache and restart the development server
