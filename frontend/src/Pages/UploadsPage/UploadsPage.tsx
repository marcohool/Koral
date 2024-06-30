import React, { FC, useEffect } from "react";
import Dashboard from "../../Components/Uploads/Dashboard/Dashboard.tsx";
import "./UploadsPage.css";
import { getUploadsAPI } from "../../Services/UploadService.ts";
import { Upload } from "../../Components/Uploads/UploadsType.ts";

interface UploadsPageProps {}

const UploadsPage: FC<UploadsPageProps> = () => {
  const [uploads, setUploads] = React.useState<Upload[]>();

  const getUploads = () => {
    getUploadsAPI().then((res) => {
      res?.data && setUploads(res?.data);
    });
  };

  useEffect(() => {
    getUploads();
  }, []);

  return (
    <div className="uploads-page__dashboard">
      <Dashboard uploads={uploads} title="All Uploads" />
    </div>
  );
};

export default UploadsPage;
