import { FC } from "react";
import "./Dashboard.css";
import { Upload } from "../UploadsType.ts";
import Card from "../Card/Card.tsx";
import Spinner from "../../Spinner/Spinner.tsx";

interface UploadsDashboardProps {
  title: string;
  uploads?: Upload[];
}

const Dashboard: FC<UploadsDashboardProps> = ({ uploads, title }) => {
  return (
    <div className="uploads-dashboard">
      <div className="uploads-dashboard__titles">
        <h3 className="uploads-dashboard__titles-title">{title}</h3>
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
