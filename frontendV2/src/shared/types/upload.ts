import { CategorisedClothingItems } from './clothingItem';

export interface Upload {
  id: string;
  title: string;
  status: string;
  imageUrl: string;
  isFavourited: boolean;
  createdOn: string;
  lastUpdatedOn: string;
  matchedClothingItems: CategorisedClothingItems[];
}
