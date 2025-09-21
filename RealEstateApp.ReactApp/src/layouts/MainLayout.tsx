import { useState } from 'react';
import { Outlet, Link } from 'react-router-dom';

/**
 * Main layout component for the application
 */
const MainLayout = () => {
  const [mobileMenuOpen, setMobileMenuOpen] = useState(false);

  const toggleMobileMenu = () => {
    setMobileMenuOpen(!mobileMenuOpen);
  };

  return (
    <div className="app-container">
      <header className="app-header app-header-fixed">
        <div className="header-content">
          <h1 className="app-title">Real Estate Application</h1>
          
          {/* Mobile menu button */}
          <button 
            className="mobile-menu-button" 
            onClick={toggleMobileMenu}
            aria-label={mobileMenuOpen ? "Close menu" : "Open menu"}
          >
            <span className="menu-icon"></span>
          </button>
        </div>
        
        <nav className={`app-nav ${mobileMenuOpen ? 'menu-open' : ''}`}>
          <Link to="/" onClick={() => setMobileMenuOpen(false)}>Home</Link>
          {/* Add more navigation links as needed */}
        </nav>
      </header>
      
      <div className="header-spacer"></div>
      
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