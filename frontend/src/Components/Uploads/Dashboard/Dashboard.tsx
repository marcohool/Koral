import { FC, useState } from "react";
import "./resources/styles/Dashboard.css";
import { Upload } from "../types.ts";
import Card from "../Card/Card.tsx";
import Spinner from "../../Spinner/Spinner.tsx";
import Button from "../../Button/Button.tsx";
import { ButtonType } from "../../Button/types.ts";
import { GoPlus } from "react-icons/go";
import UploadModal from "../UploadModal/UploadModal.tsx";
import Pagination from "./Pagination.tsx";

interface UploadsDashboardProps {
  title: string;
  uploads?: Upload[];
  pageNumber: number;
  totalPages: number;
  onNewPage: (pageNumber: number) => void;
}

const Dashboard: FC<UploadsDashboardProps> = ({
  uploads,
  title,
  pageNumber,
  totalPages,
  onNewPage,
}) => {
  const [displayUploadModal, setDisplayUploadModal] = useState<boolean>(false);

  const newUploadOnClick = () => {
    setDisplayUploadModal(true);
  };

  return (
    <div className="uploads-dashboard">
      {displayUploadModal && (
        <UploadModal
          onClose={() => {
            setDisplayUploadModal(false);
          }}
        />
      )}
      <div className="uploads-dashboard__titles">
        <h3 className="uploads-dashboard__titles-title">{title}</h3>
        <h3 className="uploads-dashboard__titles-upload-button">
          <Button
            value="New Upload"
            onClick={newUploadOnClick}
            type={ButtonType.tertiary}
            styleOverride={{
              gap: "10px",
            }}
          >
            <GoPlus />
          </Button>
        </h3>
      </div>
      {uploads ? (
        <>
          <div className="uploads-dashboard__grid">
            {uploads.map((upload) => (
              <Card key={upload.imageId} upload={upload} />
            ))}
          </div>
          <Pagination
            currentPage={pageNumber}
            totalPages={totalPages}
            onNewPage={onNewPage}
          />
        </>
      ) : (
        <div className="uploads-dashboard__spinner">
          <Spinner height="60px" />
        </div>
      )}
    </div>
  );
};

export default Dashboard;
