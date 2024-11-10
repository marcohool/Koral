import { FC } from 'react';
import { useParams } from 'react-router-dom';
import { useUpload } from 'pages/uploads/useUploads';
import ContentPage from 'shared/layouts/contentPage';
import ClothingItemCarousel from 'components/clothingItemCarousel';
import { Category, getCategoryName } from 'shared/enums/category';
import { ClothingItem } from 'shared/types/clothingItem';
import { parseDate } from 'utils/date';
import { GoHeart } from 'react-icons/go';
import Card from 'components/card';

const TopMatchesCardBody: FC<{ item: ClothingItem }> = ({ item }) => {
  return (
    <div className="p-2">
      <p className="text-xs text-ellipsis overflow-hidden line-clamp-1">
        {item.name}
      </p>
    </div>
  );
};

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
      <div className="flex max-w-content justify-center mt-10 gap-12 ">
        <img
          src={upload.imageUrl}
          alt={upload.title}
          className="object-cover h-[650px] flex-shrink-0 max-w-[50%] border border-secondary-foreground"
        />
        <div className="flex flex-col items-start justify-between text-2xl">
          <div className="flex flex-col w-full">
            <div className="flex gap-x-32 items-center w-full justify-between">
              <h2 className="uppercase">{upload.title}</h2>
              <GoHeart className="" />
            </div>
            <p className="text-xs right-0 text-muted-foreground">
              {parseDate(upload.createdOn)}
            </p>
          </div>
          <div>
            <h3 className="text-base uppercase">Top Matches</h3>
            <div className="grid grid-cols-4 mt-3">
              {upload.matchedClothingItems
                .sort((a, b) => b.similarity - a.similarity)
                .slice(0, 8)
                .map((item) => (
                  <Card
                    key={item.id}
                    imageUrl={item.imageUrl}
                    visibleBody={<TopMatchesCardBody item={item} />}
                    hoveredBody={<TopMatchesCardBody item={item} />}
                    disableHover
                    className="outline outline-1"
                  />
                ))}
            </div>
          </div>
        </div>
      </div>
      <div className="flex flex-col gap-y-7 mt-20">
        {Array.from(matchedCategories, ([category, items]) => (
          <ClothingItemCarousel
            key={category}
            title={getCategoryName(category)}
            data={items.sort((a, b) => b.similarity - a.similarity)}
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
