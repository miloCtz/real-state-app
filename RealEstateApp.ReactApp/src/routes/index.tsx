import { createBrowserRouter } from 'react-router-dom';

// Layouts
import MainLayout from '../layouts/MainLayout';

// Pages
import Home from '../pages/Home';
import PropertyDetail from '../pages/PropertyDetail';

/**
 * Application routes configuration
 */
export const router = createBrowserRouter([
  {
    path: '/',
    element: <MainLayout />,
    children: [
      {
        index: true,
        element: <Home />,
      },
      {
        path: 'property/:id',
        element: <PropertyDetail />,
      },
    ],
  },
]);

export default router;