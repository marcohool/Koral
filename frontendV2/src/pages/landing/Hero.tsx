import { FC } from 'react';
import Button from 'components/button';

const Hero: FC = () => {
  return (
    <>
      <h1 className="text-8xl text-center max-w-4xl font-butler font-medium">
        Find your dream closet using AI
      </h1>
      <Button variant="transparent" size="2xl">
        Get Started
      </Button>
    </>
  );
};

export default Hero;
