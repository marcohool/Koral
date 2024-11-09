import { FC } from 'react';
import { ClothingItem } from 'shared/types/clothingItem';
import { getCurrencyCodeName } from 'shared/enums/currencyCode';

const ClothingItemCarouselCard: FC<{ item: ClothingItem }> = ({ item }) => {
  return (
    <div className="flex flex-col h-96 border-r border-t border-b border-secondary-foreground">
      <img src={item.imageUrl} className="object-cover overflow-hidden"></img>
      <div className="flex flex-col p-2">
        <h1>{item.name}</h1>
        <p>{item.brand}</p>
        <p>
          {getCurrencyCodeName(item.currencyCode)}
          {item.price}
        </p>
        <p>{item.similarity}</p>
        <p>{item.sourceUrl}</p>
      </div>
    </div>
  );
};

export default ClothingItemCarouselCard;
