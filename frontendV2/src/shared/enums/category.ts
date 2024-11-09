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

const CategoryNames = {
  [Category.HAT]: 'Hats',
  [Category.SCARF]: 'Scarfs',
  [Category.COAT]: 'Coats',
  [Category.JACKET]: 'Jackets',
  [Category.SWEATER]: 'Sweaters',
  [Category.TOP]: 'Tops',
  [Category.HOODIE]: 'Hoodies',
  [Category.DRESS]: 'Dresses',
  [Category.GLOVES]: 'Gloves',
  [Category.BAG]: 'Bags',
  [Category.JEANS]: 'Jeans',
  [Category.BOTTOMS]: 'Bottoms',
  [Category.SHORTS]: 'Shorts',
  [Category.SKIRT]: 'Skirts',
  [Category.BELT]: 'Belts',
  [Category.SOCKS]: 'Socks',
  [Category.SHOES]: 'Shoes',
  [Category.ACCESSORIES]: 'Accessories',
  [Category.OTHER]: 'Other',
};

export function getCategoryName(category: Category) {
  return CategoryNames[category];
}
