import { FC, ReactNode } from 'react';
import { cn } from 'utils/utils';
import { Upload } from 'pages/uploads/useUploads';

const CardHeader: FC<{
  title: string;
  subtitle: string;
  className?: string;
}> = ({ title, subtitle, className }) => {
  return (
    <div className={cn(className)}>
      <h3 className="font-medium leading-none">{title}</h3>
      <p className="text-xs text-muted-foreground">{subtitle}</p>
    </div>
  );
};

const CardImage: FC<{ children: ReactNode; className?: string }> = ({
  children,
  className,
}) => {
  return <div className={cn('overflow-hidden', className)}>{children}</div>;
};

const UploadCard: FC<{
  upload: Upload;
  className?: string;
}> = ({ upload, className }) => {
  return (
    <div
      className={cn(
        'relative cursor-pointer overflow-hidden',
        'border border-transparent',
        'aspect-[7/8] flex flex-col',
        'hover:border-black',
        className,
      )}
    >
      <CardImage className="h-[100%]">
        <img
          src={upload.imageUrl}
          alt={upload.title}
          className="object-cover w-full h-full"
        />
      </CardImage>
      <CardHeader
        title={upload.title}
        subtitle={upload.title}
        className="px-2 py-3 text-sm"
      />
    </div>
  );
};

export default UploadCard;
