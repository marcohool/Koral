import { Category } from 'shared/enums/category';

export interface ClothingItem {
  id: string;
  name: string;
  description: string;
  brand: string;
  category: Category;
  gender: string;
  imageUrl: string;
  sourceUrl: string;
  sourceRegion: string;
}
