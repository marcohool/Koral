import React from "react";
import UploadCard from "../../Uploads/UploadCard/UploadCard.tsx";
import "./UploadGrid.css";
import { Upload } from "../../Uploads/UploadsType.ts";

interface Props {
  uploads: Upload[];
}

const UploadGrid: React.FC<Props> = ({ uploads }) => {
  return (
    <div className="upload__grid">
      {uploads.map((upload) => (
        <div key={upload.imageId} className="upload__grid-item">
          <UploadCard upload={upload} />
        </div>
      ))}
    </div>
  );
};

export default UploadGrid;
