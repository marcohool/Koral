import React, { FC, useEffect } from "react";
import UploadsDashboard from "../../Components/UploadsDashboard/UploadsDashboard.tsx";
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
    // setUploads([
    //   { imageUrl: "test", imageId: "1" },
    //   { imageUrl: "test", imageId: "1" },
    //   { imageUrl: "test", imageId: "1" },
    //   { imageUrl: "test", imageId: "1" },
    // ]);
  };

  useEffect(() => {
    getUploads();
  }, []);

  return (
    <div className="uploads-page__dashboard">
      <UploadsDashboard uploads={uploads} />
    </div>
  );
};

export default UploadsPage;
