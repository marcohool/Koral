import React, { FC, useEffect } from "react";
import Dashboard from "../../../../../Components/Uploads/Dashboard/Dashboard.tsx";
import "./AllUploads.css";
import { getUploadsAPI } from "../../../../../Services/UploadService.ts";
import { Upload } from "../../../../../Components/Uploads/types.ts";
import { UploadPageProps } from "../GetUploads.tsx";

const AllUploads: FC<UploadPageProps> = ({
  currentPageNumber,
  totalPages,
  onNewPage,
}) => {
  const [uploads, setUploads] = React.useState<Upload[]>();

  const getPageUploads = (pageNumber: number) => {
    setUploads(undefined);
    getUploadsAPI(pageNumber).then((res) => {
      if (res?.data) {
        setUploads(res?.data);
      }
    });
  };

  useEffect(() => {
    getPageUploads(currentPageNumber);
  }, [currentPageNumber]);

  return (
    <div className="uploads-page__dashboard">
      <Dashboard
        uploads={uploads}
        title="All Uploads"
        pageNumber={currentPageNumber}
        totalPages={totalPages}
        onNewPage={onNewPage}
      />
    </div>
  );
};

export default AllUploads;
