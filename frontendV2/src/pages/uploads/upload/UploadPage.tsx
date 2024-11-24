import { FC } from 'react';
import { useParams } from 'react-router-dom';
import { useUpload } from 'pages/uploads/useUploads';
import ContentPage from 'shared/layouts/contentPage';
import ClothingItemCarousel from 'components/clothingItemCarousel';
import { getCategoryName } from 'shared/enums/category';
import Skeleton from 'components/skeleton';
import UploadHero from './UploadHero';
import { useFavourite } from 'shared/hooks/useFavourites';

const UploadPageContent: FC = () => {
  const { id } = useParams<{ id: string }>();
  const { data: upload } = useUpload(id ?? '');
  const { mutate } = useFavourite();

  const handleFavourite = () => {
    if (upload) {
      mutate(upload.id);
    }
  };

  return (
    <>
      <UploadHero upload={upload} onFavourite={handleFavourite} />
      <div className="flex flex-col mt-20 w-full">
        {upload ? (
          upload.matchedClothingItems.map((item) => (
            <ClothingItemCarousel
              key={item.category}
              title={getCategoryName(item.category)}
              data={item.itemMatches ?? []}
            />
          ))
        ) : (
          <Skeleton className="w-full h-96 mt-20 rounded-none" />
        )}
      </div>
    </>
  );
};

const UploadPage: FC = () => {
  return <ContentPage content={<UploadPageContent />} />;
};

export default UploadPage;
