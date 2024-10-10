import { FC } from 'react';

const HeroFooter: FC<{ className?: string }> = ({ className }) => {
  return (
    <div className={className}>
      <h2 className="max-w-4xl text-center text-2xl">
        Lorem ipsum dolor sit amet, consectetur adipiscing elit. Curabitur
        ultrices malesuada enim ut varius. Curabitur lacinia mauris semper massa
        luctus, non elementum diam hendrerit.
      </h2>
    </div>
  );
};

export default HeroFooter;
