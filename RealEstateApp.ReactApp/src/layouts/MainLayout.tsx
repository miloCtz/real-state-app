import { Outlet } from 'react-router-dom';

/**
 * Main layout component for the application
 */
const MainLayout = () => {
  return (
    <div className="app-container">
      <header className="app-header">
        <h1 className="app-title">Real Estate App</h1>
        <nav className="app-nav">
          {/* Navigation items could be added here */}
        </nav>
      </header>
      
      <main className="app-main">
        <Outlet />
      </main>
      
      <footer className="app-footer">
        <p>&copy; {new Date().getFullYear()} Real Estate App. All rights reserved.</p>
      </footer>
    </div>
  );
};

export default MainLayout;