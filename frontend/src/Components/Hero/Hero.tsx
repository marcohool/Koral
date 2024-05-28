import React from "react";
import "./Hero.css";
import { FaArrowDown } from "react-icons/fa";
import { Parallax, ParallaxLayer } from "@react-spring/parallax";

interface Props {}

const Hero: React.FC<Props> = () => {
  return (
    <div className="hero__section">
      <Parallax className="parallax__main" pages={2}>
        <ParallaxLayer offset={0} factor={1.4}>
          <video
            className="background-video"
            src="/resources/videos/Hero-Video.mp4"
            autoPlay
            loop
            muted
          />
        </ParallaxLayer>
        <ParallaxLayer offset={0} factor={1} sticky={{ start: 0, end: 0.4 }}>
          <div className="hero__content">
            <h1 className="hero__title">Find your dream closet using AI</h1>
            <button className="hero__button btn-primary-outline">
              Get Started
              <FaArrowDown className="hero__button-arrow" />
            </button>
          </div>
        </ParallaxLayer>
      </Parallax>
    </div>
  );
};

export default Hero;
