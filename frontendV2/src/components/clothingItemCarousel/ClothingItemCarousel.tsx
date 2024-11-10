import { FC } from 'react';
import {
  Carousel,
  CarouselContent,
  CarouselItem,
  CarouselNext,
  CarouselPrevious,
} from 'components/carousel';
import { cn } from 'utils/utils';
import { ClothingItem } from 'shared/types/clothingItem';
import Card from 'components/card';
import { getBrandImagePath } from 'shared/helpers/getBrandImagePath';

const CardBody: FC<{ item: ClothingItem }> = ({ item }) => {
  const brandImagePath = getBrandImagePath(item.brand);

  return (
    <div className="flex flex-col p-2">
      <h1>{item.name}</h1>
      {brandImagePath ? (
        <div>
          <img src={brandImagePath} alt={item.brand} className="max-h-5" />
        </div>
      ) : (
        item.brand
      )}
      <p>
        {item.currencyCode}
        {item.price}
      </p>
      <p>{item.similarity}</p>
      <p>{item.sourceUrl}</p>
    </div>
  );
};

const ClothingItemCarousel: FC<{
  data: ClothingItem[];
  className?: string;
  title?: string;
}> = ({ data, className, title }) => {
  return (
    <div className={cn(className)}>
      <h2 className="text-2xl mb-6 ml-12 uppercase">{title}</h2>
      <Carousel
        opts={{
          align: 'start',
        }}
        className={cn('w-full')}
      >
        <CarouselContent className="border-l border-secondary-foreground">
          {data.map((item, index) => (
            <CarouselItem key={index} className="basis-1/6">
              <Card
                imageUrl={item.imageUrl}
                visibleBody={<CardBody item={item} />}
                hoveredBody={<CardBody item={item} />}
                className="outline outline-1 outline-secondary-foreground"
              />
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
