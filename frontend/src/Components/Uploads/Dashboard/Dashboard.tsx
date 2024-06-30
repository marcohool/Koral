import { FC } from "react";
import "./Dashboard.css";
import { Upload } from "../UploadsType.ts";
import Card from "../Card/Card.tsx";
import Spinner from "../../Spinner/Spinner.tsx";
import Button from "../../Button/Button.tsx";
import { ButtonType } from "../../Button/types.ts";
import { GoPlus } from "react-icons/go";

interface UploadsDashboardProps {
  title: string;
  uploads?: Upload[];
}

const Dashboard: FC<UploadsDashboardProps> = ({ uploads, title }) => {
  const newUploadOnClick = () => {
    console.log("new upload");
  };

  return (
    <div className="uploads-dashboard">
      <div className="uploads-dashboard__titles">
        <h3 className="uploads-dashboard__titles-title">{title}</h3>
        <h3 className="uploads-dashboard__titles-upload-button">
          <Button
            value="New Upload"
            onClick={newUploadOnClick}
            type={ButtonType.tertiary}
            styleOverride={{
              fontSize: "1.15rem",
              gap: "10px",
            }}
          >
            <GoPlus />
          </Button>
        </h3>
      </div>
      {uploads ? (
        <div className="uploads-dashboard__grid">
          {uploads.map((upload) => (
            <Card key={upload.imageId} upload={upload} />
          ))}
        </div>
      ) : (
        <div className="uploads-dashboard__spinner">
          <Spinner height="60px" />
        </div>
      )}
    </div>
  );
};

export default Dashboard;
