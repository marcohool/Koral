import { Category } from 'shared/enums/category';
import { CurrencyCode } from 'shared/enums/currencyCode';

export interface CategorisedClothingItems {
  category: Category;
  itemMatches: ClothingItem[];
}

export interface ClothingItem {
  id: string;
  name: string;
  brand: string;
  category: Category;
  price: number;
  currencyCode: CurrencyCode;
  imageUrl: string;
  sourceUrl: string;
  embeddingSimilarity: number;
  colourSimilarity: number;
  overallSimilarity: number;
}
