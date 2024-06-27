export type Upload = {
  imageId: string;
  imageTitle: string;
  imagePath: string;
  createdAt: string;
  status: StatusType;
  isFavourited: boolean;
  isPinned: boolean;
  accuracyRating?: number;
  clothingItemsMatched: number;
};

export enum StatusType {
  Processing = 0,
  Processed = 1,
  Failed = 2,
}
