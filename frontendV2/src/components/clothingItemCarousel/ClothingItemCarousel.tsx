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
import { getCurrencyCodeName } from 'shared/enums/currencyCode';
import { GoArrowUpRight } from 'react-icons/go';

const CardBody: FC<{ item: ClothingItem; isHovered: boolean }> = ({
  item,
  isHovered,
}) => {
  const brandImagePath = getBrandImagePath(item.brand);

  return (
    <div className="flex flex-col p-2 gap-y-2">
      <h1>{item.name}</h1>
      {brandImagePath ? (
        <div>
          <img src={brandImagePath} alt={item.brand} className="max-h-5" />
        </div>
      ) : (
        item.brand
      )}
      <div className="flex items-center justify-between">
        <p>
          {getCurrencyCodeName(item.currencyCode)}
          {item.price}
        </p>
        {isHovered && (
          <a
            className="flex items-center gap-x-0.5 line-clamp-1 whitespace-nowrap border-b border-secondary-foreground"
            href={item.sourceUrl}
            target="_blank"
          >
            <p className="text-sm text-secondary-foreground">
              Visit on Retailers Website
            </p>
            {<GoArrowUpRight size={14} />}
          </a>
        )}
      </div>
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
        className={cn('w-full hover:z-50')}
      >
        <CarouselContent className="border-l border-secondary-foreground">
          {data.map((item, index) => (
            <CarouselItem key={index} className="basis-1/6">
              <Card
                imageUrl={item.imageUrl}
                visibleBody={<CardBody item={item} isHovered={false} />}
                hoveredBody={<CardBody item={item} isHovered={true} />}
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
