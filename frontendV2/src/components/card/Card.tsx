import { FC, ReactNode, useState } from 'react';
import { cn } from 'utils/utils';
import { ScrollArea } from 'components/scrollArea';

interface CardProps {
  imageUrl: string;
  className?: string;
  visibleBody: ReactNode;
  hoveredBody: ReactNode;
  onClick?: () => void;
  disableHover?: boolean;
}

const CardImage: FC<{ children: ReactNode; className?: string }> = ({
  children,
  className,
}) => {
  return (
    <div className={cn('overflow-hidden', className)}>
      <ScrollArea className="h-full">{children}</ScrollArea>
    </div>
  );
};

const Card: FC<CardProps> = ({
  imageUrl,
  className,
  visibleBody,
  hoveredBody,
  onClick,
  disableHover,
}) => {
  const [isHovered, setIsHovered] = useState(false);

  return (
    <div
      className={cn(
        'flex flex-col aspect-[8/11] overflow-hidden bg-background w-full relative',
        'hover:z-40',
        className,
      )}
      style={{
        height: disableHover
          ? '100%'
          : isHovered
            ? 'calc(100% + 100px)'
            : '100%',
      }}
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
      {isHovered ? hoveredBody : visibleBody}
    </div>
  );
};

export default Card;
