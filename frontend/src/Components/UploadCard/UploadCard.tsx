import React from "react";
import { Upload } from "../RecentUploads/types.ts";
import "./UploadCard.css";

interface Props {
  upload: Upload;
}

const UploadCard: React.FC<Props> = ({ upload }) => {
  return (
    <div className="upload__card">
      {upload.imageId} - {upload.imagePath}
    </div>
  );
};

export default UploadCard;
