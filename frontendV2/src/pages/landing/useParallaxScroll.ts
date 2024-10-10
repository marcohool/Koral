import { RefObject, useEffect, useState } from 'react';
import { normaliseToRange } from 'utils/useMath';

interface ParallaxRef {
  current: number;
  space: number;
}

export function useParallaxScroll(parallaxRef: RefObject<ParallaxRef>) {
  const [width, setWidth] = useState({ width: '100%' });
  const [isScrolled, setIsScrolled] = useState(false);

  useEffect(() => {
    const handleScroll = () => {
      if (parallaxRef.current) {
        const scrollY = parallaxRef.current.current;
        const parallaxPageHeight = parallaxRef.current.space;

        const widthValue = Math.max(
          100 -
            normaliseToRange(
              scrollY,
              parallaxPageHeight / 4,
              parallaxPageHeight,
            ) *
              40,
          80,
        );
        setWidth({ width: `${widthValue}%` });

        if (scrollY === 0) {
          setIsScrolled(false);
        } else {
          setIsScrolled(true);
        }
      }
    };

    window.addEventListener('scroll', handleScroll);

    return () => {
      window.removeEventListener('scroll', handleScroll);
    };
  }, [parallaxRef]);

  return { width, isScrolled };
}
