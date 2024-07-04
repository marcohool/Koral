import React, { FC, useEffect } from "react";
import { getUploadsCountAPI } from "../../../../Services/UploadService.ts";
import AllUploads from "./All/AllUploads.tsx";
import FavouriteUploads from "./Favourite/FavouriteUploads.tsx";
import { UploadType } from "./types.ts";

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
  const [pageNumber, setPageNumber] = React.useState<number>(1);
  const [totalPages, setTotalPages] = React.useState<number>(1);

  useEffect(() => {
    const getTotalUploadsCount = () => {
      getUploadsCountAPI(type).then((res) => {
        if (res?.data) {
          setTotalPages(Math.ceil(res?.data / maxUploadsPerPage));
        }
      });
    };

    getTotalUploadsCount();
  }, [type]);

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
