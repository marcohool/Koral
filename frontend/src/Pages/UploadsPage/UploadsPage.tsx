import React, { FC, useEffect } from "react";
import UploadsDashboard from "../../Components/Uploads/UploadsDashboard/UploadsDashboard.tsx";
import "./UploadsPage.css";
import { uploadsGET } from "../../Services/UploadService.ts";
import { Upload } from "../../Components/Uploads/UploadsType.ts";

interface UploadsPageProps {}

const UploadsPage: FC<UploadsPageProps> = () => {
  const [uploads, setUploads] = React.useState<Upload[]>();

  const getUploads = () => {
    uploadsGET().then((res) => {
      res?.data && setUploads(res?.data);
    });
  };

  useEffect(() => {
    getUploads();
  }, []);

  return (
    <div className="uploads-page__dashboard">
      <UploadsDashboard uploads={uploads} title="All Uploads" />
    </div>
  );
};

export default UploadsPage;
