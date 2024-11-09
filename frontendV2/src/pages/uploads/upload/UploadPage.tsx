import { FC } from 'react';
import { useParams } from 'react-router-dom';
import { useUpload } from 'pages/uploads/useUploads';
import ContentPage from 'shared/layouts/contentPage';
import ClothingItemCarousel from 'components/clothingItemCarousel';
import { Category, getCategoryName } from 'shared/enums/category';
import { ClothingItem } from 'shared/types/clothingItem';

const UploadPageContent: FC = () => {
  const { id } = useParams<{ id: string }>();
  const { data: upload } = useUpload(id ?? '');

  if (!upload) {
    return null;
  }

  const matchedCategories = upload.matchedClothingItems.reduce((acc, item) => {
    const category = item.category;
    if (!acc.has(category)) {
      acc.set(category, []);
    }
    acc.get(category)!.push(item);
    return acc;
  }, new Map<Category, ClothingItem[]>());

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
        {Array.from(matchedCategories, ([category, items]) => (
          <ClothingItemCarousel
            key={category}
            title={getCategoryName(category)}
            data={items}
          />
        ))}
      </div>
    </>
  );
};

const UploadPage: FC = () => {
  return <ContentPage content={<UploadPageContent />} />;
};

export default UploadPage;
