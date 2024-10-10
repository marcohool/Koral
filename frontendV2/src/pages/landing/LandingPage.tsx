import { FC, useCallback, useEffect, useRef, useState } from 'react';
import { IParallax, Parallax, ParallaxLayer } from '@react-spring/parallax';
import BackgroundVideo from 'pages/landing/BackgroundVideo';
import Navbar from 'components/navbar';
import Hero from './Hero';
import HeroHelper from './HeroHelper';

const PARALLAX_PAGES = 4;

const LandingPage: FC = () => {
  const [scrollY, setScrollY] = useState(0);
  const [parallaxPageHeight, setParallaxPageHeight] = useState(0);
  const parallax = useRef<IParallax>(null);

  const handleScroll = useCallback(() => {
    if (parallax.current) {
      setScrollY(parallax.current.current);
      setParallaxPageHeight(parallax.current.space);
    }
  }, []);

  useEffect(() => {
    const container = parallax.current?.container
      .current as HTMLDivElement | null;

    container?.addEventListener('scroll', handleScroll);
    return () => {
      container?.removeEventListener('scroll', handleScroll);
    };
  }, [handleScroll, parallax, parallax.current?.container]);

  return (
    <>
      <Navbar scrolled={scrollY == 0} />
      <Parallax
        ref={parallax}
        pages={PARALLAX_PAGES}
        className="top-0 left-0 -z-10"
        style={{ overflow: 'hidden scroll' }}
      >
        <ParallaxLayer offset={0} factor={1.6} className="flex justify-center">
          <BackgroundVideo
            scrollY={scrollY}
            pageHeight={parallaxPageHeight}
            totalPages={PARALLAX_PAGES}
          />
        </ParallaxLayer>
        <ParallaxLayer offset={0} factor={1} sticky={{ start: 0, end: 0.4 }}>
          <div className="flex flex-col items-center justify-center h-full gap-24 text-background">
            <Hero />
          </div>
        </ParallaxLayer>
        <ParallaxLayer offset={1.6} factor={0.4}>
          <div className="flex h-full items-center justify-center">
            <HeroHelper />
          </div>
        </ParallaxLayer>
        <ParallaxLayer offset={2} factor={2}>
          <div className=""></div>
        </ParallaxLayer>
      </Parallax>
    </>
  );
};

export default LandingPage;
