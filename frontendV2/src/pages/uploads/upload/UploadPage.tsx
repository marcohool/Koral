import { FC } from 'react';
import { useParams } from 'react-router-dom';
import { useUpload } from 'pages/uploads/useUploads';
import ContentPage from 'shared/layouts/contentPage';
import ClothingItemCarousel from 'components/clothingItemCarousel';
import { Category, getCategoryName } from 'shared/enums/category';
import { ClothingItem } from 'shared/types/clothingItem';
import Skeleton from 'components/skeleton';
import UploadHero from './UploadHero';

const UploadPageContent: FC = () => {
  const { id } = useParams<{ id: string }>();
  const { data: upload } = useUpload(id ?? '');

  const matchedCategories = upload?.matchedClothingItems.reduce((acc, item) => {
    const category = item.category;
    if (!acc.has(category)) {
      acc.set(category, []);
    }
    acc.get(category)!.push(item);
    return acc;
  }, new Map<Category, ClothingItem[]>());

  return (
    <>
      <UploadHero upload={upload} />
      <div className="flex flex-col mt-20 w-full">
        {(matchedCategories &&
          Array.from(matchedCategories, ([category, items]) => (
            <ClothingItemCarousel
              key={category}
              title={getCategoryName(category)}
              data={items.sort((a, b) => b.similarity - a.similarity)}
            />
          ))) ?? (
          <>
            <Skeleton className="mb-6 ml-12 h-20 w-72 mt-20" />
            <Skeleton className="w-full h-96" />
          </>
        )}
      </div>
    </>
  );
};

const UploadPage: FC = () => {
  return <ContentPage content={<UploadPageContent />} />;
};

export default UploadPage;
