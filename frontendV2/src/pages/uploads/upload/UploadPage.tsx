import { FC } from 'react';
import { useParams } from 'react-router-dom';
import { Upload, useUpload } from 'pages/uploads/useUploads';
import ContentPage from '@/shared/layouts/contentPage';

const UploadPageContent: FC = () => {
  const { id } = useParams<{ id: string }>();
  const { data: upload, isLoading } = useUpload(id ?? '');

  if (!upload) {
    return null;
  }

  console.log(upload);

  return (
    <>
      <div className="flex w-full justify-center mt-10 gap-12">
        <img
          src={upload.imageUrl}
          alt={upload.title}
          className="object-contain h-[650px] flex-shrink-0"
        />
        <div className="flex flex-col items-start">
          <h2 className="text-xl">{upload.title}</h2>
          <p>Matched clothing items: {upload.matchedClothingItems.length}</p>
          <p>Uploaded on: {new Date(upload.createdOn).toDateString()}</p>
          <p>Identified clothing items:</p>
          <p>Accuracy rating: 5</p>
        </div>
      </div>
      <div
        className="w-screen !max-w-none mt-10 bg-gray-100 absolute"
        style={{ left: '0' }}
      >
        test
      </div>
    </>
  );
};

const UploadPage: FC = () => {
  return <ContentPage content={<UploadPageContent />} />;
};

export default UploadPage;
