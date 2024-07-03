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

export enum NewUploadFileState {
  Valid = 0,
  Invalid = 1,
}

export interface HelperText {
  message: string;
  type?: NewUploadFileState;
}
