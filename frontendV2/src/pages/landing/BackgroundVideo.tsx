import { FC, RefObject, useCallback, useEffect } from 'react';
import { animated, useSpring } from '@react-spring/web';
import { normaliseToRange } from 'utils/useMath';
import { IParallax } from '@react-spring/parallax';

const BackgroundVideo: FC<{
  parallax: RefObject<IParallax>;
  minWidth: number;
  acceleration: number;
  totalPages: number;
}> = ({ parallax, minWidth, acceleration, totalPages }) => {
  const [{ width }, setWidth] = useSpring(() => ({ width: '100%' }));

  const handleScroll = useCallback(() => {
    if (parallax.current) {
      const scrollY = parallax.current.current;
      const parallaxPageHeight = parallax.current.space;

      const backgroundVideoWidth = Math.max(
        100 -
          normaliseToRange(
            scrollY,
            parallaxPageHeight / totalPages,
            parallaxPageHeight,
          ) *
            acceleration,
        minWidth,
      );

      void setWidth.start({ width: `${backgroundVideoWidth}%` });
    }
  }, [acceleration, minWidth, parallax, setWidth, totalPages]);

  useEffect(() => {
    const container = parallax.current?.container
      .current as HTMLDivElement | null;

    container?.addEventListener('scroll', handleScroll);
    return () => {
      container?.removeEventListener('scroll', handleScroll);
    };
  }, [handleScroll, parallax, parallax.current?.container]);

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
