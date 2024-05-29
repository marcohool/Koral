import React, { useEffect, useRef } from "react";
import "./Hero.css";
import { FaArrowDown } from "react-icons/fa";
import { animated, useSpring } from "@react-spring/web";
import { IParallax, Parallax, ParallaxLayer } from "@react-spring/parallax";

interface Props {}

const Hero: React.FC<Props> = () => {
  const parallax = useRef<IParallax>(null);
  const [{ width }, setWidth] = useSpring(() => ({ width: "100%" }));

  const handleScroll = () => {
    if (parallax.current) {
      console.log(parallax.current.current);
      const scrollY = parallax.current.current;
      const widthValue = Math.max(100 - scrollY * 0.05, 80);
      setWidth({ width: `${widthValue}%` });
    }
  };

  useEffect(() => {
    if (parallax.current) {
      const container = parallax.current.container.current as HTMLElement;
      container.addEventListener("scroll", handleScroll);
      return () => {
        container.removeEventListener("scroll", handleScroll);
      };
    }
  }, []);

  return (
    <div className="hero__section">
      <Parallax ref={parallax} className="parallax__main" pages={2}>
        <ParallaxLayer offset={0} factor={1.6}>
          <animated.video
            className="background-video"
            src="/resources/videos/Hero-Video.mp4"
            autoPlay
            loop
            muted
            style={{ width: width.to((w) => w) }}
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
        <ParallaxLayer offset={1.6} factor={0.4}>
          <div className="hero__end">
            <h2 className="hero__end-title">
              Lorem ipsum dolor sit amet, consectetur adipiscing elit. Curabitur
              ultrices malesuada enim ut varius. Curabitur lacinia mauris semper
              massa luctus, non elementum diam hendrerit.
            </h2>
          </div>
        </ParallaxLayer>
      </Parallax>
    </div>
  );
};

export default Hero;
