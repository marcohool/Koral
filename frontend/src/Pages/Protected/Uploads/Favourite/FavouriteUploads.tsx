import React, { FC, useEffect } from "react";
import { Upload } from "../../../../Components/Uploads/types.ts";
import { getFavouriteUploadsAPI } from "../../../../Services/UploadService.ts";
import Dashboard from "../../../../Components/Uploads/Dashboard/Dashboard.tsx";

interface FavouriteUploadsProps {}

const FavouriteUploads: FC<FavouriteUploadsProps> = () => {
  const [uploads, setUploads] = React.useState<Upload[]>();

  const getUploads = () => {
    getFavouriteUploadsAPI().then((res) => {
      res?.data && setUploads(res?.data);
    });
  };

  useEffect(() => {
    getUploads();
  }, []);

  return (
    <div className="uploads-page__dashboard">
      <Dashboard uploads={uploads} title="Favourites" />
    </div>
  );
};

export default FavouriteUploads;
