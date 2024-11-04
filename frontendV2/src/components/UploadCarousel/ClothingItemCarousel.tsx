import { FC } from 'react';
import {
  Carousel,
  CarouselContent,
  CarouselItem,
  CarouselNext,
  CarouselPrevious,
} from 'components/carousel';
import { ClothingItem } from 'pages/uploads/useUploads';
import { cn } from 'utils/utils';

const ClothingItemCarousel: FC<{
  data: ClothingItem[];
  className?: string;
  title?: string;
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
        <CarouselContent>
          {data.map((item, index) => (
            <CarouselItem key={index} className="basis-1/6">
              <img src={item.imageUrl} />
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
