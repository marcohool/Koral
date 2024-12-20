import { FC } from 'react';
import { cn } from 'utils/utils';
import Card from 'components/card';
import { Upload } from 'shared/types/upload';
import globalRouter from 'App/globalRouter';
import { ClothingItem } from 'shared/types/clothingItem';

const CardBody: FC<{
  title: string;
  subtitle: string;
  matchedClothingItems?: ClothingItem[];
  className?: string;
  hovered?: boolean;
}> = ({ title, subtitle, matchedClothingItems, className, hovered }) => {
  return (
    <div className="flex flex-col gap-2 p-3">
      {hovered && (
        <div className="grid grid-cols-4 max-h-16 overflow-hidden">
          {matchedClothingItems?.map((ci) => {
            return (
              <div
                key={ci.id}
                className="relative w-full h-full overflow-hidden"
              >
                <img
                  src={ci.imageUrl}
                  alt={ci.name}
                  className="object-cover w-full h-full hover:scale-125"
                />
              </div>
            );
          })}
        </div>
      )}
      <div className={cn(className, 'text-ellipsis overflow-hidden')}>
        <h3 className="font-medium leading-none line-clamp-1 text-ellipsis">
          {title}
        </h3>
        <p className="text-xs text-muted-foreground line-clamp-1">{subtitle}</p>
      </div>
    </div>
  );
};

const UploadGrid: FC<{
  uploads?: Upload[];
  className?: string;
}> = ({ uploads, className }) => {
  if (!uploads || uploads.length === 0) {
    return <>No uploads!</>;
  }

  return (
    <div className={cn('mb-28', className)}>
      {uploads.map((upload) => (
        <Card
          key={upload.id}
          imageUrl={upload.imageUrl}
          onClick={() => globalRouter.navigate?.('/uploads/' + upload.id)}
          className="hover:cursor-pointer hover:border hover:border-secondary-foreground"
          visibleBody={
            <CardBody
              title={upload.title}
              subtitle={upload.title}
              className="text-sm"
            />
          }
          hoveredBody={
            <CardBody
              title={upload.title}
              subtitle={upload.title}
              className="text-sm"
              matchedClothingItems={upload.matchedClothingItems.flatMap(
                (item) => item.itemMatches,
              )}
              hovered={true}
            />
          }
        />
      ))}
    </div>
  );
};

export default UploadGrid;
