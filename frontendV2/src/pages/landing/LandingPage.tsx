import { FC, useCallback, useEffect, useRef, useState } from 'react';
import { IParallax, Parallax, ParallaxLayer } from '@react-spring/parallax';
import BackgroundVideo from './Hero/BackgroundVideo';
import Hero from './Hero/Hero';
import HeroFooter from './Hero/HeroFooter';
import About from './About/About';
import { Page } from 'App/router.types';
import { LandingNavbar } from 'components/navbar';

const PARALLAX_PAGES = 4;
const NavbarPages: Page[] = [Page.About, Page.Contact, Page.Login, Page.SignUp];

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
      <LandingNavbar scrolled={scrollY == 0} pages={NavbarPages} />
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
          <Hero className="flex flex-col items-center justify-center h-full gap-24 text-background" />
        </ParallaxLayer>
        <ParallaxLayer offset={1.6} factor={0.4}>
          <HeroFooter className="flex h-full items-center justify-center" />
        </ParallaxLayer>
        <ParallaxLayer offset={2} factor={2}>
          <About scrollY={scrollY} pageHeight={parallaxPageHeight} />
        </ParallaxLayer>
      </Parallax>
    </>
  );
};

export default LandingPage;
