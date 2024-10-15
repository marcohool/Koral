import { FC } from 'react';
import useUploads, { Upload } from 'pages/uploads/useUploads';
import UploadCard from 'components/uploadCard';
import { cn } from 'utils/utils';

const UploadGrid: FC<{ uploads?: Upload[]; className?: string }> = ({
  uploads,
  className,
}) => {
  return (
    <>
      {uploads && uploads.length > 0 ? (
        <div className={cn('flex space-x-4 pb-4', className)}>
          {uploads.map((upload, index) => (
            <UploadCard
              key={index}
              upload={upload}
              width={375}
              aspectRatio="square"
            />
          ))}
        </div>
      ) : (
        <p>None</p>
      )}
    </>
  );
};

const HomePage: FC = () => {
  const { data, isLoading } = useUploads();

  if (isLoading) {
    return <p>Loading...</p>;
  }

  return (
    <div>
      <UploadGrid uploads={data} className="flex justify-center mt-20" />
    </div>
  );
};

export default HomePage;
