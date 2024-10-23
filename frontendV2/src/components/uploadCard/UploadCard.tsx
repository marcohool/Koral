import { FC, ReactNode, useState } from 'react';
import { cn } from 'utils/utils';
import { Upload } from 'pages/uploads/useUploads';

const CardHeader: FC<{
  title: string;
  subtitle: string;
  className?: string;
  hovered?: boolean;
}> = ({ title, subtitle, className }) => {
  return (
    <div className={cn(className, 'text-ellipsis overflow-hidden')}>
      <h3 className="font-medium leading-none line-clamp-2">{title}</h3>
      <p className="text-xs text-muted-foreground line-clamp-1">{subtitle}</p>
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
        'md:border border-transparent',
        'aspect-[8/11] flex flex-col',
        'hover:border-black hover:z-20 ',
        className,
      )}
      style={{ height: `${isHovered ? '130%' : '100%'}` }}
      onMouseEnter={() => setIsHovered(true)}
      onMouseLeave={() => setIsHovered(false)}
    >
      <div className="absolute top-0 left-0 w-full h-full">
        <CardImage className="h-[80%]">
          <img
            src={upload.imageUrl}
            alt={upload.title}
            className="object-cover w-full h-full"
          />
        </CardImage>
        <CardBody
          isHovered={isHovered}
          className="h-[20%]"
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
              hovered={true}
            />
          }
        ></CardBody>
      </div>
    </div>
  );
};

export default UploadCard;
