import React from "react";
import { Upload } from "../types.ts";

interface Props {
  uploads: Upload[];
}

const UploadGrid: React.FC<Props> = ({ uploads }) => {
  return (
    <div>
      {uploads.map((upload) => (
        <div key={upload.id}>{upload.title}</div>
      ))}
    </div>
  );
};

export default UploadGrid;
