export enum Category {
  HAT = 0,
  SCARF = 1,
  COAT = 2,
  JACKET = 3,
  SWEATER = 4,
  TOP = 5,
  HOODIE = 6,
  DRESS = 7,
  GLOVES = 8,
  BAG = 9,
  JEANS = 10,
  BOTTOMS = 11,
  SHORTS = 12,
  SKIRT = 13,
  BELT = 14,
  SOCKS = 15,
  SHOES = 16,
  ACCESSORIES = 17,
  OTHER = 18,
}

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

export interface Upload {
  id: string;
  title: string;
  status: string;
  imageUrl: string;
  isFavourited: boolean;
  createdOn: string;
  lastUpdatedOn: string;
  matchedClothingItems: ClothingItem[];
}
