import React from "react";
import "./UploadCard.css";
import { Upload } from "../UploadsType.ts";
import StatusCircle from "../StatusCircle/StatusCircle.tsx";

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
      <div className={"upload__card__content"}>
        <div className="upload__card-titles">
          <h3 className="upload__card-titles-title">
            Placeholder Image Title Value
          </h3>
          <div className="upload__card-titles-status">
            <StatusCircle status={upload.status} />
          </div>
        </div>
      </div>
      {/*{upload.createdAt}*/}
      {/*{upload.isFavourited}*/}
      {/*{upload.isPinned}*/}
      {/*{upload.clothingItemsMatched}*/}
    </div>
  );
};

export default UploadCard;
