import { FC } from 'react';
import useUploads, { Upload } from 'pages/uploads/useUploads';
import { Navbar } from 'components/navbar';
import Divider from 'components/divider';
import SortingMenu from 'components/navbar/SortingMenu';
import Button from 'components/button';
import UploadDialog from 'components/uploadDialog';
import { GoPlus } from 'react-icons/go';
import UploadGrid from 'components/uploadGrid';
import Spinner from 'components/spinner';

const HomeContent: FC = () => {
  const { data, isLoading, error } = useUploads();

  if (isLoading) {
    return <Spinner height={24} />;
  }

  if (error) {
    return 'Error';
  }

  return (
    <>
      <div className="flex justify-between items-center">
        <h1 className="py-2.5 text-xl font-normal">Uploads</h1>
        <UploadDialog>
          <Button variant="outline">
            <GoPlus className="mr-2" />
            New Upload
          </Button>
        </UploadDialog>
      </div>
      <Divider />
      <SortingMenu />
      <UploadGrid
        uploads={data}
        className="grid gap-[0.5px] sm:gap-2 md:px-2 xl:px-0 max-w-content mx-auto grid-cols-2 md:grid-cols-3 xl:grid-cols-5"
        isLoading={false}
      />
    </>
  );
};

const HomePage: FC = () => {
  return (
    <>
      <Navbar />
      <section className="max-w-content mx-auto flex flex-col gap-4 relative">
        <div className="mt-2 sm:mt-8 flex items-center w-full justify-center text-primary/[.6]">
          <p className="text-xs/loose">{'-----------<----(@'}</p>
        </div>
        <HomeContent />
      </section>
    </>
  );
};

export default HomePage;
