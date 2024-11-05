import { FC } from 'react';
import { useParams } from 'react-router-dom';
import { useUpload } from 'pages/uploads/useUploads';
import ContentPage from '@/shared/layouts/contentPage';
import ClothingItemCarousel from 'components/UploadCarousel';
import _ from 'lodash';
import { Category } from 'pages/uploads/types';

const UploadPageContent: FC = () => {
  const { id } = useParams<{ id: string }>();
  const { data: upload, isLoading } = useUpload(id ?? '');

  if (!upload) {
    return null;
  }

  const matchedCategories = _.groupBy(upload.matchedClothingItems, 'category');
  console.log(matchedCategories);
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
      <div className="flex flex-col gap-y-7 mt-20">
        {Object.entries(matchedCategories).map(([category, items]) => {
          const categoryText = Category[category as keyof typeof Category];
          return (
            <ClothingItemCarousel
              key={category}
              title={categoryText}
              data={items}
            />
          );
        })}
      </div>
    </>
  );
};

const UploadPage: FC = () => {
  return <ContentPage content={<UploadPageContent />} />;
};

export default UploadPage;
