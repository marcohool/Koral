import React, { FC, useEffect } from "react";
import { Upload } from "../../../../../Components/Uploads/types.ts";
import { getFavouriteUploadsAPI } from "../../../../../Services/UploadService.ts";
import Dashboard from "../../../../../Components/Uploads/Dashboard/Dashboard.tsx";
import { UploadPageProps } from "../GetUploads.tsx";

const FavouriteUploads: FC<UploadPageProps> = ({
  currentPageNumber,
  totalPages,
  onNewPage,
}) => {
  const [uploads, setUploads] = React.useState<Upload[]>();

  const getPageFavouriteUploads = (pageNumber: number) => {
    setUploads(undefined);
    getFavouriteUploadsAPI(pageNumber).then((res) => {
      if (res?.data) {
        setUploads(res?.data);
      }
    });
  };

  useEffect(() => {
    getPageFavouriteUploads(currentPageNumber);
  }, [currentPageNumber]);

  return (
    <div className="uploads-page__dashboard">
      <Dashboard
        uploads={uploads}
        title="Favourites"
        pageNumber={currentPageNumber}
        totalPages={totalPages}
        onNewPage={onNewPage}
      />
    </div>
  );
};

export default FavouriteUploads;
