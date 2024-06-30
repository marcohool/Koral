import React, { useEffect, useRef, useState } from "react";
import "./LandingPage.css";
import Navbar from "../../../Components/Navbar/Navbar.tsx";
import { IParallax, Parallax, ParallaxLayer } from "@react-spring/parallax";
import { animated, useSpring } from "@react-spring/web";
import { FaArrowDown } from "react-icons/fa";

interface Props {}

const LandingPage: React.FC<Props> = () => {
  const parallax = useRef<IParallax>(null);
  const [isScrolled, setIsScrolled] = useState(false);
  const [{ width }, setWidth] = useSpring(() => ({ width: "100%" }));

  // Function to scale a number between two values to 0-1
  function transformNumber(
    input: number,
    scaleStart: number,
    scaleEnd: number,
  ) {
    if (input > scaleEnd) input = scaleEnd;

    if (input < scaleStart) return 0;

    return (input - scaleStart) / (scaleEnd - scaleStart);
  }

  useEffect(() => {
    const handleScroll = () => {
      if (parallax.current) {
        const scrollY = parallax.current.current;
        const parallaxPageHeight = parallax.current.space;

        const widthValue = Math.max(
          100 -
            transformNumber(
              scrollY,
              parallaxPageHeight / 4,
              parallaxPageHeight,
            ) *
              40,
          80,
        );
        setWidth({ width: `${widthValue}%` });

        if (scrollY == 0) {
          setIsScrolled(false);
        } else {
          setIsScrolled(true);
        }

        const opacity =
          1 -
          transformNumber(scrollY, parallaxPageHeight, parallaxPageHeight * 2);
        const element = document.querySelector(
          ".product__overview",
        ) as HTMLElement;
        element!.style.opacity = opacity.toString();
      }
    };

    if (parallax.current) {
      const container = parallax.current.container.current as HTMLElement;
      container.addEventListener("scroll", handleScroll);
      return () => {
        container.removeEventListener("scroll", handleScroll);
      };
    }
  }, [setWidth]);

  return (
    <div>
      <Navbar isScrolled={isScrolled} />
      {/* Cannot separate Parallax layers into components due to react-spring limitations */}

      {/*Hero Section*/}
      <div className="hero__section">
        <Parallax ref={parallax} className="parallax__main" pages={4}>
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
          {/*End of hero section*/}

          {/*Product Overview Section*/}
          <ParallaxLayer offset={2} factor={2}>
            <div className="product__overview"></div>
          </ParallaxLayer>
          {/*Product Overview Section*/}
        </Parallax>
      </div>
    </div>
  );
};

export default LandingPage;
