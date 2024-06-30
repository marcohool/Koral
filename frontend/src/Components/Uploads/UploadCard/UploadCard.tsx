import React, { useState } from "react";
import "./resources/styles/UploadCard.css";
import { StatusType, Upload } from "../UploadsType.ts";
import { ParseDate } from "../helpers.ts";
import FavouriteButton from "./FavouriteButton.tsx";

const API_URL = "https://localhost:5001/";

interface Props {
  upload: Upload;
}

const UploadCard: React.FC<Props> = ({ upload }) => {
  const [isFavourited, setFavourite] = useState<boolean>(upload.isFavourited);
  const error = upload.status === StatusType.Failed ? "error" : "";

  const handleFavourite = () => {
    setFavourite(!isFavourited);
  };

  return (
    <div className={`upload__card ${error} `}>
      <div className="upload__card-image">
        <img src={API_URL + upload.imagePath} alt={upload.imageId} />
        <FavouriteButton
          isFavourited={isFavourited}
          setFavourite={handleFavourite}
        />
      </div>
      <div className="upload__card__content">
        <div className="upload__card-titles">
          <h3 className="upload__card-titles-title">
            Placeholder Image Title Value
          </h3>
          <h3 className="upload__card-titles-subtitle">
            {ParseDate(upload.createdAt)}
          </h3>
        </div>
        <div className="upload__card-details"></div>
      </div>
    </div>
  );
};

export default UploadCard;
