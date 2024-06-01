import React, { useEffect, useRef, useState } from "react";
import "./HomePage.css";
import Navbar from "../../Components/Navbar/Navbar.tsx";
import { IParallax, Parallax, ParallaxLayer } from "@react-spring/parallax";
import { animated, useSpring } from "@react-spring/web";
import { FaArrowDown } from "react-icons/fa";

interface Props {}

const HomePage: React.FC<Props> = () => {
  const parallax = useRef<IParallax>(null);
  const [isScrolled, setIsScrolled] = useState(false);
  const [{ width }, setWidth] = useSpring(() => ({ width: "100%" }));

  function transformNumber(input: number, scaleStart: number) {
    if (input < 0) input = 0;
    if (input > 1000) input = 1000;

    if (input < scaleStart) return 0;

    return ((input - scaleStart) / (1000 - scaleStart)) * 1000;
  }

  const handleScroll = () => {
    if (parallax.current) {
      const scrollY = parallax.current.current;

      const widthValue = Math.max(
        100 - transformNumber(scrollY, 400) * 0.05,
        80,
      );
      setWidth({ width: `${widthValue}%` });

      if (scrollY == 0) {
        setIsScrolled(false);
      } else {
        setIsScrolled(true);
      }
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
    <div>
      <Navbar isScrolled={isScrolled} />
      {/* Cannot separate Parallax layers into components due to react-spring limitations */}
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
                Lorem ipsum dolor sit amet, consectetur adipiscing elit.
                Curabitur ultrices malesuada enim ut varius. Curabitur lacinia
                mauris semper massa luctus, non elementum diam hendrerit.
              </h2>
            </div>
          </ParallaxLayer>
        </Parallax>
      </div>
    </div>
  );
};

export default HomePage;
