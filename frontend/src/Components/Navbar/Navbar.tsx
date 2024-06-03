import React, { useEffect, useState } from "react";
import "./Navbar.css";
import { Link } from "react-router-dom";

interface Props {
  isScrolled: boolean;
}

const Navbar: React.FC<Props> = ({ isScrolled }) => {
  const [navActive, setNavActive] = useState(false);
  const [forceScrolled, setForceScrolled] = useState(false);

  const toggleNav = () => {
    setNavActive(!navActive);
  };

  const closeMenu = () => {
    setNavActive(false);
  };

  useEffect(() => {
    const handleResize = () => {
      if (window.innerWidth <= 500) {
        closeMenu();
      }

      if (window.innerWidth <= 700) {
        setForceScrolled(true);
      } else {
        setForceScrolled(false);
      }
    };
    window.addEventListener("resize", handleResize);

    return () => {
      window.removeEventListener("resize", handleResize);
    };
  }, []);

  useEffect(() => {
    if (window.innerWidth <= 1200) {
      closeMenu();
    }
  }, []);

  return (
    <nav
      className={`navbar ${navActive ? "active" : ""}  ${isScrolled ? "scrolled" : ""} ${forceScrolled ? "scrolled" : ""}`}
    >
      <div className="navbar__start">Koral</div>
      <a
        className={`nav__hamburger ${navActive ? "active" : ""}`}
        onClick={toggleNav}
      >
        <span className="nav__hamburger__line"></span>
        <span className="nav__hamburger__line"></span>
        <span className="nav__hamburger__line"></span>
      </a>
      <div className={`navbar__end ${navActive ? "active" : ""} `}>
        <ul>
          <Link to="/login">
            <li>Log in</li>
          </Link>
        </ul>
        <ul>
          <Link to="/signup">
            <li>Sign Up</li>
          </Link>
        </ul>
      </div>
    </nav>
  );
};

export default Navbar;
