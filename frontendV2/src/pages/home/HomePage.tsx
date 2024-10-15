import { FC } from 'react';
import useUploads, { Upload } from 'pages/uploads/useUploads';
import UploadCard from 'components/uploadCard';

const UploadGrid: FC<{
  cardHeight: number;
  uploads?: Upload[];
  className?: string;
}> = ({ uploads, className }) => {
  if (!uploads || uploads.length === 0) {
    return <>No uploads!</>;
  }

  return (
    <div className={className}>
      {uploads.map((upload) => (
        <UploadCard key={upload.id} upload={upload} />
      ))}
    </div>
  );
};

const HomePage: FC = () => {
  const { data, isLoading } = useUploads();

  if (isLoading) {
    return <p>Loading...</p>;
  }

  return (
    <div>
      <UploadGrid
        uploads={data}
        className="grid grid-cols-4 mt-20 gap-2 max-w-7xl mx-auto"
        cardHeight={500}
      />
    </div>
  );
};

export default HomePage;
