import { FC } from 'react';
import useUploads, { Upload } from 'pages/uploads/useUploads';
import UploadCard from 'components/uploadCard';
import { Navbar } from 'components/navbar';
import Divider from 'components/divider';
import SortingMenu from 'components/navbar/SortingMenu';
import Button from 'components/button';
import UploadDialog from 'components/uploadDialog';
import { GoPlus } from 'react-icons/go';

const UploadGrid: FC<{
  cardHeight: number;
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
    <div className={className}>
      {uploads.map((upload) => (
        <UploadCard key={upload.id} upload={upload} />
      ))}
    </div>
  );
};

const HomePage: FC = () => {
  const { data, isLoading, error } = useUploads();

  return (
    <>
      <Navbar />
      <section className="max-w-content mx-auto flex flex-col gap-4">
        <div className="mt-2 sm:mt-8 flex items-center w-full justify-center text-primary/[.6]">
          <p className="text-xs/loose">{'-----------<----(@'}</p>
        </div>
        <div className="flex justify-between items-center">
          <h1 className="py-2.5 text-xl font-normal">Uploads</h1>
          <UploadDialog>
            <Button variant="outline">
              <GoPlus className="mr-2" />
              New Upload
            </Button>
          </UploadDialog>
        </div>
        <Divider className="" />
        <SortingMenu />
        <UploadGrid
          uploads={data}
          className="grid gap-[0.5px] sm:gap-2 md:px-2 xl:px-0 max-w-content mx-auto grid-cols-2 md:grid-cols-3 xl:grid-cols-5"
          isLoading={isLoading}
          cardHeight={500}
        />
      </section>
    </>
  );
};

export default HomePage;
