import zaraImage from 'assets/images/brands/zara.png';

const BrandImage: Record<string, string> = {
  Zara: zaraImage,
};

export const getBrandImagePath = (brand: string) => {
  return BrandImage[brand];
};
