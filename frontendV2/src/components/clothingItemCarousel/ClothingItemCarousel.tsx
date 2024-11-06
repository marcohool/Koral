import { FC } from 'react';
import {
  Carousel,
  CarouselContent,
  CarouselItem,
  CarouselNext,
  CarouselPrevious,
} from 'components/carousel';
import { cn } from 'utils/utils';
import { Category, ClothingItem } from 'pages/uploads/types';
import ClothingItemCarouselCard from 'components/clothingItemCarousel/ClothingItemCarouselCard';

const ClothingItemCarousel: FC<{
  data: ClothingItem[];
  className?: string;
  title?: Category;
}> = ({ data, className, title }) => {
  return (
    <div className={cn(className)}>
      <h2 className="text-2xl m-6">{title}</h2>
      <Carousel
        opts={{
          align: 'start',
        }}
        className={cn('w-full')}
      >
        <CarouselContent className="border-l border-secondary-foreground">
          {data.map((item, index) => (
            <CarouselItem key={index} className="basis-1/6">
              <ClothingItemCarouselCard item={item} />
            </CarouselItem>
          ))}
        </CarouselContent>
        <CarouselPrevious />
        <CarouselNext />
      </Carousel>
    </div>
  );
};

export default ClothingItemCarousel;
