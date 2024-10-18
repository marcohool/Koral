import { FC } from 'react';
import Button from 'components/button';
import { Link } from 'react-router-dom';

const Hero: FC<{ className?: string }> = ({ className }) => {
  return (
    <div className={className}>
      <h1 className="text-8xl text-center max-w-4xl font-medium">
        Find your dream closet using AI
      </h1>
      <Link to="/signup">
        <Button variant="transparent" size="2xl">
          Get Started
        </Button>
      </Link>
    </div>
  );
};

export default Hero;
