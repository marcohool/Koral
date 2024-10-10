import { FC } from 'react';
import { normaliseToRange } from 'utils/useMath';

const About: FC<{ scrollY: number; pageHeight: number }> = ({
  scrollY,
  pageHeight,
}) => {
  const opacity = 1 - normaliseToRange(scrollY, pageHeight, pageHeight * 2);

  return (
    <div className="h-full bg-black" style={{ opacity }}>
      About
    </div>
  );
};

export default About;
