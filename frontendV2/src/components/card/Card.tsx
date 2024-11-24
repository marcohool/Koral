import { FC, ReactNode, useRef, useState } from 'react';
import { cn } from 'utils/utils';
import { ScrollArea } from 'components/scrollArea';
import useScrollOnDrag from 'react-scroll-ondrag';

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
  const containerRef = useRef(null);
  const { events } = useScrollOnDrag(containerRef);

  return (
    <div
      className={cn('overflow-hidden', className)}
      {...events}
      ref={containerRef}
    >
      {children}
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
    >
      <CardImage className="flex-grow cursor-grab">
        <img
          src={imageUrl}
          alt="Card image"
          className="object-cover w-full h-full pointer-events-none"
          style={{ minHeight: isHovered ? '130%' : '100%' }}
        />
      </CardImage>
      <div onClick={onClick}>{isHovered ? hoveredBody : visibleBody}</div>
    </div>
  );
};

export default Card;
