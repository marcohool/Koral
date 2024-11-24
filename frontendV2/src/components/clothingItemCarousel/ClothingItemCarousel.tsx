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
import Progress from 'components/progress';
import {
  Tooltip,
  TooltipContent,
  TooltipProvider,
  TooltipTrigger,
} from 'components/tooltip';

const CardBody: FC<{ item: ClothingItem; isHovered: boolean }> = ({
  item,
  isHovered,
}) => {
  const brandImagePath = getBrandImagePath(item.brand);

  const similarity = Math.round(item.overallSimilarity * 100);

  return (
    <div className="flex flex-col gap-y-2 p-3">
      <TooltipProvider delayDuration={0}>
        <Tooltip>
          <TooltipTrigger asChild>
            <a className="flex items-center gap-3 mb-1.5">
              <Progress value={similarity} />
              {isHovered && <p className="text-sm">{similarity}%</p>}
            </a>
          </TooltipTrigger>
          <TooltipContent>
            <p className="text-sm">Similarity rating</p>
          </TooltipContent>
        </Tooltip>
      </TooltipProvider>
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
            className="flex items-center gap-x-0.5 line-clamp-1 whitespace-nowrap border-b border-secondary-foreground hover:text-primary hover:border-primary"
            href={item.sourceUrl}
            target="_blank"
          >
            <p className="text-sm">Visit on Retailers Website</p>
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
  cardBodyOverride?: FC<{ item: ClothingItem; isHovered: boolean }>;
  disableHover?: boolean;
}> = ({ data, className, title, cardBodyOverride, disableHover }) => {
  return (
    <div className={cn(className)}>
      {title && <h2 className="text-2xl mt-6 mb-6 ml-12 uppercase">{title}</h2>}
      <Carousel
        opts={{
          align: 'start',
        }}
        className={cn('w-full hover:z-50')}
      >
        <CarouselContent className="border-l border-secondary-foreground">
          {data
            .sort((a, b) => b.overallSimilarity - a.overallSimilarity)
            .map((item, index) => (
              <CarouselItem key={index} className="basis-1/6">
                <Card
                  imageUrl={item.imageUrl}
                  visibleBody={
                    cardBodyOverride ? (
                      cardBodyOverride({ item, isHovered: false })
                    ) : (
                      <CardBody item={item} isHovered={false} />
                    )
                  }
                  hoveredBody={
                    cardBodyOverride ? (
                      cardBodyOverride({ item, isHovered: true })
                    ) : (
                      <CardBody item={item} isHovered={true} />
                    )
                  }
                  className="outline outline-1 outline-secondary-foreground"
                  disableHover={disableHover}
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
