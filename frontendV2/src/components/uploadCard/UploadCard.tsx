import { FC, HTMLAttributes } from 'react';
import { cn } from 'utils/utils';
import { Upload } from 'pages/uploads/useUploads';

interface UploadCardProps extends HTMLAttributes<HTMLDivElement> {
  upload: Upload;
  aspectRatio?: 'portrait' | 'square';
  width?: number;
  height?: number;
}

const UploadCard: FC<UploadCardProps> = ({
  upload,
  width,
  height,
  aspectRatio = 'portrait',
  className,
  ...props
}) => {
  return (
    <div className={cn('space-y-3', className)} {...props}>
      <div className="overflow-hidden rounded-md">
        <img
          src={upload.imageUrl}
          alt={upload.title}
          width={width}
          height={height}
          className={cn(
            'object-cover transition-all hover:scale-105',
            aspectRatio === 'portrait' ? 'aspect-[9/10]' : 'aspect-square',
          )}
        />
      </div>
      <div className="space-y-1 text-sm">
        <h3 className="font-medium leading-none">{upload.title}</h3>
        <p className="text-xs text-muted-foreground">{upload.title}</p>
      </div>
    </div>
  );
};

export default UploadCard;
