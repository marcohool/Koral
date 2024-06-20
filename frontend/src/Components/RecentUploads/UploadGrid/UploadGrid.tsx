import React from "react";
import { Upload } from "../types.ts";
import UploadCard from "../../UploadCard/UploadCard.tsx";
import "./UploadGrid.css";

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
