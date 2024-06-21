import React from "react";
import { Upload } from "../RecentUploads/types.ts";
import "./UploadCard.css";

const API_URL = "https://localhost:5001/";

interface Props {
  upload: Upload;
}

const UploadCard: React.FC<Props> = ({ upload }) => {
  return (
    <div className="upload__card">
      <div className="upload__card-image">
        <img src={API_URL + upload.imagePath} alt={upload.imageId} />
      </div>
      <div className="upload__card-titles">
        <h3>Placeholder Image Title Value</h3>
      </div>
    </div>
  );
};

export default UploadCard;
