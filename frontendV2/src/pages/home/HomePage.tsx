import { FC } from 'react';
import useUploads from 'pages/uploads/useUploads';
import Divider from 'components/divider';
import SortingMenu from 'components/navbar/SortingMenu';
import Button from 'components/button';
import UploadDialog from 'components/uploadDialog';
import { GoAlert, GoPlus } from 'react-icons/go';
import UploadGrid from 'components/uploadGrid';
import Spinner from 'components/spinner';
import Alert, { AlertDescription, AlertTitle } from 'components/alert';
import ContentPage from 'shared/layouts/contentPage';

const HomeContent: FC = () => {
  const { data, isLoading, error } = useUploads();

  if (isLoading || error) {
    return (
      <div className="mt-[20vh] w-full">
        {isLoading ? (
          <Spinner height={24} />
        ) : (
          <div className="relative">
            <Alert
              variant="destructive"
              className="max-w-screen-sm absolute left-0 right-0 ml-auto mr-auto"
            >
              <GoAlert size={16} />
              <AlertTitle>Error</AlertTitle>
              <AlertDescription className="flex flex-col">
                <span>
                  An unexpected error has occurred. Please try again later
                </span>
                <span>{error?.message}</span>
              </AlertDescription>
            </Alert>
          </div>
        )}
      </div>
    );
  }

  return (
    <div className="flex flex-col max-w-content mx-auto gap-4">
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
      />
    </div>
  );
};

const HomePage: FC = () => {
  return (
    <ContentPage heading={'-----------<----(@'} content={<HomeContent />} />
  );
};

export default HomePage;
