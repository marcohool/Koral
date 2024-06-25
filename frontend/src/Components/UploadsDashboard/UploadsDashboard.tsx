import { FC } from "react";
import "./UploadsDashboard.css";
import { Upload } from "../Uploads/UploadsType";
import UploadCard from "../Uploads/UploadCard/UploadCard.tsx";

interface UploadsDashboardProps {
  uploads?: Upload[];
}

const UploadsDashboard: FC<UploadsDashboardProps> = ({ uploads }) => {
  return (
    <div className="uploads-dashboard">
      <div className="uploads-dashboard__titles">
        <h3 className="uploads-dashboard__titles-title">Uploads</h3>
      </div>
      <div className="uploads-dashboard__grid">
        {uploads?.map((upload) => (
          <div key={upload.imageId} className="upload__grid-item">
            <UploadCard upload={upload} />
          </div>
        ))}
      </div>
    </div>
  );
};

export default UploadsDashboard;
