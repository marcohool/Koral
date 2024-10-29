import { FC, ReactNode, useState } from 'react';
import { cn } from 'utils/utils';
import { ClothingItem, Upload } from 'pages/uploads/useUploads';

const CardHeader: FC<{
  title: string;
  subtitle: string;
  matchedClothingItems?: ClothingItem[];
  className?: string;
  hovered?: boolean;
}> = ({ title, subtitle, matchedClothingItems, className, hovered }) => {
  return (
    <div className="flex flex-col gap-2">
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
        <h3 className="font-medium leading-none line-clamp-2">{title}</h3>
        <p className="text-xs text-muted-foreground line-clamp-1">{subtitle}</p>
      </div>
    </div>
  );
};

const CardImage: FC<{ children: ReactNode; className?: string }> = ({
  children,
  className,
}) => {
  return <div className={cn('overflow-hidden', className)}>{children}</div>;
};

const CardBody: FC<{
  isHovered?: boolean;
  visible: ReactNode;
  hovered: ReactNode;
  className?: string;
}> = ({ isHovered, visible, hovered, className }) => {
  return (
    <div className={cn('p-2', className)}>{isHovered ? hovered : visible}</div>
  );
};

const UploadCard: FC<{
  upload: Upload;
  className?: string;
}> = ({ upload, className }) => {
  const [isHovered, setIsHovered] = useState(false);

  return (
    <div
      className={cn(
        'relative cursor-pointer overflow-hidden bg-background w-full',
        'border border-transparent',
        'aspect-[8/11] flex flex-col',
        'hover:border-black hover:z-20 ',
        className,
      )}
      style={{ height: isHovered ? 'calc(100% + 100px)' : '100%' }}
      onMouseEnter={() => setIsHovered(true)}
      onTouchStart={() => setIsHovered(true)}
      onMouseLeave={() => setIsHovered(false)}
      onTouchEnd={() => setIsHovered(false)}
    >
      <div className="relative w-full h-full flex flex-col">
        <CardImage className="flex-grow">
          <img
            src={upload.imageUrl}
            alt={upload.title}
            className="object-cover w-full h-full"
            style={{ minHeight: isHovered ? '130%' : '100%' }}
          />
        </CardImage>
        <CardBody
          isHovered={isHovered}
          visible={
            <CardHeader
              title={upload.title}
              subtitle={upload.title}
              className="text-sm"
            />
          }
          hovered={
            <CardHeader
              title={upload.title}
              subtitle={upload.title}
              className="text-sm"
              matchedClothingItems={upload.matchedClothingItems}
              hovered={true}
            />
          }
        ></CardBody>
      </div>
    </div>
  );
};

export default UploadCard;
