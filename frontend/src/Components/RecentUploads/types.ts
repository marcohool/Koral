export enum UploadType {
  All = "ALL",
  Saved = "SAVED",
}

export type Upload = {
  id: string;
  title: string;
  description: string;
  saved: boolean;
  date: string;
};
