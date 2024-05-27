import React from "react";
import "./HomePage.css";
import Hero from "../../Components/Hero/Hero.tsx";

interface Props {}

const HomePage: React.FC<Props> = () => {
  return (
    <div>
      <Hero />
    </div>
  );
};

export default HomePage;
