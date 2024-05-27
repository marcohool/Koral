import React from "react";
import "./Hero.css";

interface Props {}

const Hero: React.FC<Props> = () => {
  return (
    <section className="hero__section">
      <video
        className="background-video"
        src="/resources/videos/Hero-Video.mp4"
        autoPlay
        loop
        muted
      />
      <div className="hero__content">
        <h1 className="hero__title">Find your dream closet using AI</h1>
        <button className="hero__button">Get Started</button>
      </div>
    </section>
  );
};

export default Hero;
