import { FC } from 'react';
import { ClothingItem } from 'shared/types/clothingItem';
import { getCurrencyCodeName } from 'shared/enums/currencyCode';
import { getBrandImagePath } from 'shared/helpers/getBrandImagePath';

const ClothingItemCarouselCard: FC<{ item: ClothingItem }> = ({ item }) => {
  const brandImagePath = getBrandImagePath(item.brand);

  return (
    <div className="flex flex-col h-96 border-r border-t border-b border-secondary-foreground">
      <img src={item.imageUrl} className="object-cover overflow-hidden"></img>
      <div className="flex flex-col p-2">
        <h1>{item.name}</h1>
        {brandImagePath ? (
          <div>
            <img src={brandImagePath} alt={item.brand} className="max-h-5" />
          </div>
        ) : (
          item.brand
        )}
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
