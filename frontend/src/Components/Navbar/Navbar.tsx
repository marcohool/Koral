import React, { useEffect, useState } from "react";
import "./Navbar.css";

interface Props {
  isScrolled: boolean;
}

const Navbar: React.FC<Props> = ({ isScrolled }) => {
  const [navActive, setNavActive] = useState(false);

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
      className={`navbar ${navActive ? "active" : ""}  ${isScrolled ? "scrolled background-blur " : ""}`}
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
          <li>Log in</li>
        </ul>
        <ul>
          <li>Sign Up</li>
        </ul>
      </div>
    </nav>
  );
};

export default Navbar;
