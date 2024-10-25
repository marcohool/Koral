import { FC } from 'react';
import { Upload } from 'pages/uploads/useUploads';
import UploadCard from 'components/uploadCard';
import { cn } from 'utils/utils';

const UploadGrid: FC<{
  isLoading: boolean;
  uploads?: Upload[];
  className?: string;
}> = ({ uploads, className, isLoading }) => {
  if (isLoading) {
    return <p>Loading...</p>;
  }

  if (!uploads || uploads.length === 0) {
    return <>No uploads!</>;
  }

  return (
    <div className={cn('mb-28', className)}>
      {uploads.map((upload) => (
        <UploadCard key={upload.id} upload={upload} />
      ))}
    </div>
  );
};

export default UploadGrid;
