import { FC } from 'react';
import useUploads, { Upload } from 'pages/uploads/useUploads';
import UploadCard from 'components/uploadCard';
import Navbar from 'components/navbar';

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
    <>
      <Navbar scrolled={false} className="max-w-7xl" />
      <UploadGrid
        uploads={data}
        className="grid mt-20 gap-[0.5px] sm:gap-2 md:px-2 xl:px-0 max-w-7xl mx-auto grid-cols-2 md:grid-cols-3 xl:grid-cols-4"
        cardHeight={500}
      />
    </>
  );
};

export default HomePage;
