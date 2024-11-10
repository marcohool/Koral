import { FC, ReactNode, useState } from 'react';
import { cn } from 'utils/utils';

interface CardProps {
  imageUrl: string;
  className?: string;
  visibleBody: ReactNode;
  hoveredBody: ReactNode;
  onClick?: () => void;
}

const CardImage: FC<{ children: ReactNode; className?: string }> = ({
  children,
  className,
}) => {
  return <div className={cn('overflow-hidden', className)}>{children}</div>;
};

const CardBody: FC<{
  className?: string;
  children: ReactNode;
}> = ({ className, children }) => {
  return <div className={cn('p-2', className)}>{children}</div>;
};

const Card: FC<CardProps> = ({
  imageUrl,
  className,
  visibleBody,
  hoveredBody,
  onClick,
}) => {
  const [isHovered, setIsHovered] = useState(false);

  return (
    <div
      className={cn(
        'flex flex-col aspect-[8/11] relative overflow-hidden bg-background w-full z-0',
        'hover:z-50',
        className,
      )}
      style={{ height: isHovered ? 'calc(100% + 100px)' : '100%' }}
      onMouseEnter={() => setIsHovered(true)}
      onMouseLeave={() => setIsHovered(false)}
      onTouchStart={() => setIsHovered(true)}
      onTouchEnd={() => setIsHovered(false)}
      onClick={onClick}
    >
      <CardImage className="flex-grow">
        <img
          src={imageUrl}
          alt="Card image"
          className="object-cover w-full h-full"
          style={{ minHeight: isHovered ? '130%' : '100%' }}
        />
      </CardImage>
      <CardBody>{isHovered ? hoveredBody : visibleBody}</CardBody>
    </div>
  );
};

export default Card;
