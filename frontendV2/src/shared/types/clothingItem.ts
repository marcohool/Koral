import { Category } from 'shared/enums/category';
import { CurrencyCode } from 'shared/enums/currencyCode';

export interface ClothingItem {
  id: string;
  name: string;
  brand: string;
  category: Category;
  price: number;
  currencyCode: CurrencyCode;
  imageUrl: string;
  sourceUrl: string;
  similarity: number;
}
