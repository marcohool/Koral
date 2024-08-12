import React, { FC, useEffect } from "react";
import AllUploads from "./All/AllUploads.tsx";
import FavouriteUploads from "./Favourite/FavouriteUploads.tsx";
import { UploadType } from "./types.ts";
import { getUploadsAPI } from "../../../../Services/UploadService.ts";
import { Upload } from "../../../../Components/Uploads/types.ts";

interface GetUploadsProps {
  type: UploadType;
}

export interface UploadPageProps {
  currentPageNumber: number;
  totalPages: number;
  onNewPage: (pageNumber: number) => void;
}

const maxUploadsPerPage = 12;

const GetUploads: FC<GetUploadsProps> = ({ type }) => {
  const [uploads, setUploads] = React.useState<Upload[]>();
  const [pageNumber, setPageNumber] = React.useState<number>(1);
  const [totalPages, setTotalPages] = React.useState<number>(1);

  const getPageUploads = (pageNumber: number) => {
    setUploads(undefined);
    getUploadsAPI(pageNumber).then((res) => {
      if (res?.data) {
        setUploads(res?.data);
      }
    });
  };

  useEffect(() => {
    getPageUploads(pageNumber);
  }, [pageNumber]);

  const handleNewPage = (pageNumber: number) => {
    setPageNumber(pageNumber);
  };

  return type === UploadType.All ? (
    <AllUploads
      currentPageNumber={pageNumber}
      totalPages={totalPages}
      onNewPage={handleNewPage}
    />
  ) : (
    <FavouriteUploads
      currentPageNumber={pageNumber}
      totalPages={totalPages}
      onNewPage={handleNewPage}
    />
  );
};

export default GetUploads;
