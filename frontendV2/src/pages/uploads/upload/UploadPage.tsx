import { FC } from 'react';
import { useParams } from 'react-router-dom';
import { useUpload } from 'pages/uploads/useUploads';
import ContentPage from 'shared/layouts/contentPage';
import ClothingItemCarousel from 'components/clothingItemCarousel';
import { Category, getCategoryName } from 'shared/enums/category';
import { ClothingItem } from 'shared/types/clothingItem';
import Skeleton from 'components/skeleton';
import UploadHero from './UploadHero';
import { useFavourite } from 'shared/hooks/useFavourites';

const UploadPageContent: FC = () => {
  const { id } = useParams<{ id: string }>();
  const { data: upload } = useUpload(id ?? '');
  const { mutate } = useFavourite();

  const matchedCategories = upload?.matchedClothingItems.reduce((acc, item) => {
    const category = item.category;
    if (!acc.has(category)) {
      acc.set(category, []);
    }
    acc.get(category)!.push(item);
    return acc;
  }, new Map<Category, ClothingItem[]>());

  const handleFavourite = () => {
    if (upload) {
      mutate(upload.id);
    }
  };

  return (
    <>
      <UploadHero upload={upload} onFavourite={handleFavourite} />
      <div className="flex flex-col mt-20 w-full">
        {matchedCategories &&
          Array.from(matchedCategories, ([category, items]) => (
            <ClothingItemCarousel
              key={category}
              title={getCategoryName(category)}
              data={items.sort((a, b) => b.similarity - a.similarity)}
            />
          ))}
        {!matchedCategories && (
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
