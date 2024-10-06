import { FC, useRef } from 'react';
import { IParallax, Parallax, ParallaxLayer } from '@react-spring/parallax';
import BackgroundVideo from 'pages/landing/BackgroundVideo';

const MIN_VIDEO_WIDTH = 80;
const VIDEO_WIDTH_ACCELERATION = 40;
const PARALLAX_PAGES = 4;

const LandingPage: FC = () => {
  const parallax = useRef<IParallax>(null);

  return (
    <Parallax ref={parallax} pages={PARALLAX_PAGES} className="top-0 left-0">
      <ParallaxLayer offset={0} factor={1.6} className="flex justify-center">
        <BackgroundVideo
          parallax={parallax}
          minWidth={MIN_VIDEO_WIDTH}
          acceleration={VIDEO_WIDTH_ACCELERATION}
          totalPages={PARALLAX_PAGES}
        />
      </ParallaxLayer>
    </Parallax>
  );
};

export default LandingPage;
