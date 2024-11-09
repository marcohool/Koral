import { FC } from 'react';
import { ClothingItem } from 'shared/types/clothingItem';

const ClothingItemCarouselCard: FC<{ item: ClothingItem }> = ({ item }) => {
  return (
    <div className="flex flex-col h-96 border-r border-t border-b border-secondary-foreground">
      <img src={item.imageUrl} className="object-cover overflow-hidden"></img>
      <div className="flex flex-col p-2">
        <h1>{item.name}</h1>
      </div>
    </div>
  );
};

export default ClothingItemCarouselCard;
