import { FC, useEffect } from 'react';
import { animated, useSpring } from '@react-spring/web';
import { normaliseToRange } from 'utils/useMath.ts';

const MIN_WIDTH = 80;
const WIDTH_ACCELERATION = 40;

const BackgroundVideo: FC<{
  scrollY: number;
  pageHeight: number;
  totalPages: number;
}> = ({ scrollY, pageHeight, totalPages }) => {
  const [{ width }, setWidth] = useSpring(() => ({ width: '100%' }));

  useEffect(() => {
    const backgroundVideoWidth = Math.max(
      100 -
        normaliseToRange(scrollY, pageHeight / totalPages, pageHeight) *
          WIDTH_ACCELERATION,
      MIN_WIDTH,
    );

    void setWidth.start({ width: `${backgroundVideoWidth}%` });
  }, [scrollY, pageHeight, totalPages, setWidth]);

  return (
    <animated.video
      className="absolute top-50 left-50 min-w-45 h-full object-cover -z-10 -transform-translate-50 rounded-b-2xl"
      src="videos/Hero-Video.mp4"
      autoPlay
      loop
      muted
      style={{ width: width }}
    />
  );
};

export default BackgroundVideo;
