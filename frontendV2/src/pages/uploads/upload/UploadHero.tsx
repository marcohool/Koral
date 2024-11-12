import { FC } from 'react';
import { Upload } from 'shared/types/upload';
import Skeleton from 'components/skeleton';
import { GoHeart } from 'react-icons/go';
import { parseDate } from 'utils/date';
import Card from 'components/card';
import { ClothingItem } from 'shared/types/clothingItem';

const UploadImage: FC<{
  imageUrl?: string;
  imageAlt?: string;
  className?: string;
}> = ({ imageUrl, imageAlt, className }) => {
  return (
    <>
      {imageUrl && imageAlt ? (
        <img src={imageUrl} alt={imageAlt} className={className} />
      ) : (
        <div>
          <Skeleton className="h-[650px] w-[550px]" />
        </div>
      )}
    </>
  );
};

const HeroContent: FC<{ upload?: Upload }> = ({ upload }) => {
  return (
    <div className="flex flex-col items-start w-full justify-between text-2xl">
      <HeroTitle
        title={upload?.title}
        createdOn={upload?.createdOn}
        onFavourite={() => {
          console.log('Favourite clicked');
        }}
      />
      <TopMatchesGrid items={upload?.matchedClothingItems} />
    </div>
  );
};

const HeroTitle: FC<{
  title?: string;
  createdOn?: string;
  onFavourite: () => void;
}> = ({ title, createdOn, onFavourite }) => {
  return (
    <div className="flex flex-col w-full">
      <div className="flex gap-x-32 items-center w-full justify-between">
        {title ? (
          <>
            <h2 className="uppercase">{title}</h2>
            <GoHeart className="" onClick={onFavourite} />
          </>
        ) : (
          <Skeleton>
            <h2 className="opacity-0">Placeholder Upload Text Skeleton</h2>
          </Skeleton>
        )}
      </div>
      {createdOn && (
        <p className="text-xs text-muted-foreground">{parseDate(createdOn)}</p>
      )}
    </div>
  );
};

const TopMatchesGrid: FC<{ items?: ClothingItem[] }> = ({ items }) => {
  const placeholderCard = (
    <Card
      imageUrl="https://placehold.co/300"
      visibleBody={<TopMatchesCardBody itemName="Placeholder name" />}
      hoveredBody={<TopMatchesCardBody itemName="Placeholder name" />}
      disableHover
      className="outline outline-1"
    />
  );

  return (
    <>
      <div>
        {items && (
          <>
            <h3 className="text-base uppercase">Top Matches</h3>
            <div className="grid grid-cols-4 mt-3">
              {items
                .sort((a, b) => b.similarity - a.similarity)
                .slice(0, 8)
                .map((item) => (
                  <Card
                    key={item.id}
                    imageUrl={item.imageUrl}
                    visibleBody={<TopMatchesCardBody itemName={item.name} />}
                    hoveredBody={<TopMatchesCardBody itemName={item.name} />}
                    disableHover
                    className="outline outline-1"
                  />
                ))}
            </div>
          </>
        )}
        {!items && (
          <Skeleton>
            <div className="grid grid-cols-4 mt-3 opacity-0">
              {Array.from({ length: 8 }, (_, i) => (
                <div key={i}>{placeholderCard}</div>
              ))}
            </div>
          </Skeleton>
        )}
      </div>
    </>
  );
};

const TopMatchesCardBody: FC<{ itemName: string }> = ({ itemName }) => {
  return (
    <div className="p-2">
      <p className="text-xs text-ellipsis overflow-hidden line-clamp-1">
        {itemName}
      </p>
    </div>
  );
};

const UploadHero: FC<{ upload?: Upload }> = ({ upload }) => {
  return (
    <div className="flex max-w-content justify-center mt-10 gap-12">
      <UploadImage
        imageUrl={upload?.imageUrl}
        imageAlt={upload?.title}
        className="object-cover h-[650px] flex-shrink-0 max-w-[50%] border border-secondary-foreground"
      />
      <HeroContent upload={upload} />
    </div>
  );
};

export default UploadHero;
