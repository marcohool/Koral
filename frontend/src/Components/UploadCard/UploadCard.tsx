import React from "react";
import { Upload } from "../RecentUploads/types.ts";

interface Props {
  upload: Upload;
}

const UploadCard: React.FC<Props> = ({ upload }) => {
  return <div>UploadCard</div>;
};

export default UploadCard;
