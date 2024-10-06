import { FC, useEffect, useRef, useState } from "react";
import { IParallax, Parallax, ParallaxLayer } from "@react-spring/parallax";
import { animated, useSpring } from "@react-spring/web";
import { normaliseToRange } from "utils/useMath.ts";

const MIN_VIDEO_WIDTH = 80;
const VIDEO_WIDTH_ACCELERATION = 40;
const PARALLAX_PAGES = 4;

const LandingPage: FC = () => {
  const parallax = useRef<IParallax>(null);
  const [{ width }, setWidth] = useSpring(() => ({ width: "103%" }));

  const handleScroll = () => {
    if (parallax.current) {
      const scrollY = parallax.current.current;
      const parallaxPageHeight = parallax.current.space;

      const backgroundVideoWidth = Math.max(
        100 -
          normaliseToRange(
            scrollY,
            parallaxPageHeight / PARALLAX_PAGES,
            parallaxPageHeight,
          ) *
            VIDEO_WIDTH_ACCELERATION,
        MIN_VIDEO_WIDTH,
      );

      console.log(backgroundVideoWidth);
      void setWidth({ width: `${backgroundVideoWidth}%` });
    }
  };

  useEffect(() => {
    const container = parallax.current?.container.current as HTMLDivElement;
    container.addEventListener("scroll", handleScroll);
    return () => {
      container.removeEventListener("scroll", handleScroll);
    };
  }, []);

  return (
    <Parallax ref={parallax} pages={PARALLAX_PAGES} className="top-0 left-0">
      <ParallaxLayer offset={0} factor={1.6}>
        <div className="flex justify-center">
          <animated.video
            className="absolute top-50 left-50 min-w-45 h-full object-cover -z-10 -transform-translate-50 rounded-b-2xl"
            src="videos/Hero-Video.mp4"
            autoPlay
            loop
            muted
            style={{ width: width, marginLeft: "-3px" }}
          />
        </div>
      </ParallaxLayer>
    </Parallax>
  );
};

export default LandingPage;
