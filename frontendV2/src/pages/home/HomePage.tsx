import { FC } from 'react';
import useUploads, { Upload } from 'pages/uploads/useUploads';
import UploadCard from 'components/uploadCard';
import { Navbar } from 'components/navbar';
import Divider from 'components/divider';
import SortingMenu from 'components/navbar/SortingMenu';

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
  const { data, isLoading, error } = useUploads();

  if (isLoading) {
    return <p>Loading...</p>;
  }

  return (
    <>
      <Navbar />
      <section className="max-w-content mx-auto flex flex-col gap-3">
        <h1 className="mt-9 py-2.5 text-xl font-normal font-butler">Uploads</h1>
        <Divider className="" />
        <SortingMenu />
        <UploadGrid
          uploads={data}
          className="grid mt-4 gap-[0.5px] sm:gap-2 md:px-2 xl:px-0 max-w-content mx-auto grid-cols-2 md:grid-cols-3 xl:grid-cols-5"
          cardHeight={500}
        />
      </section>
    </>
  );
};

export default HomePage;
