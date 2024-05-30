import React, { useState } from "react";
import "./HomePage.css";
import Hero from "../../Components/Hero/Hero.tsx";
import Navbar from "../../Components/Navbar/Navbar.tsx";

interface Props {}

const HomePage: React.FC<Props> = () => {
  const [isScrolled, setIsScrolled] = useState(false);

  const handleScrollChange = (scrolled: boolean) => {
    setIsScrolled(scrolled);
  };

  return (
    <div>
      <Navbar isScrolled={isScrolled} />
      <Hero updateScroll={handleScrollChange} />
    </div>
  );
};

export default HomePage;
