import React, { FC, useEffect } from "react";
import Dashboard from "../../../../Components/Uploads/Dashboard/Dashboard.tsx";
import "./AllUploads.css";
import {
  getUploadCountAPI,
  getUploadsAPI,
} from "../../../../Services/UploadService.ts";
import { Upload } from "../../../../Components/Uploads/types.ts";

const maxUploadsPerPage = 12;

interface UploadsPageProps {}

const AllUploads: FC<UploadsPageProps> = () => {
  const [uploads, setUploads] = React.useState<Upload[]>();
  const [pageNumber, setPageNumber] = React.useState<number>(1);
  const [totalPages, setTotalPages] = React.useState<number>(1);

  const getUploads = (pageNumber: number) => {
    setUploads([]);
    getUploadsAPI(pageNumber).then((res) => {
      if (res?.data) {
        setUploads(res?.data);
      }
    });
  };

  const getUploadCount = () => {
    getUploadCountAPI().then((res) => {
      if (res?.data) {
        setTotalPages(Math.ceil(res?.data / maxUploadsPerPage));
      }
    });
  };

  const handleNewPage = (pageNumber: number) => {
    setPageNumber(pageNumber);
  };

  useEffect(() => {
    getUploadCount();
  }, []);

  useEffect(() => {
    getUploads(pageNumber);
  }, [pageNumber]);

  return (
    <div className="uploads-page__dashboard">
      <Dashboard
        uploads={uploads}
        title="All Uploads"
        pageNumber={pageNumber}
        totalPages={totalPages}
        onNewPage={handleNewPage}
      />
    </div>
  );
};

export default AllUploads;
