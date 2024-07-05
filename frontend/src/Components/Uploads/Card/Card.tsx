import React, { useState } from "react";
import "./resources/styles/Card.css";
import { StatusType, Upload } from "../types.ts";
import { ParseDate } from "../helpers.ts";
import FavouriteButton from "./FavouriteButton.tsx";
import { toast } from "react-toastify";
import { favouriteUploadAPI } from "../../../Services/UploadService.ts";
import { Link } from "react-router-dom";

const API_URL = "https://localhost:5001/";

interface Props {
  upload: Upload;
}

const Card: React.FC<Props> = ({ upload }) => {
  const [isFavourited, setFavourite] = useState<boolean>(upload.isFavourited);
  const error = upload.status === StatusType.Failed ? "error" : "";

  const handleFavourite = () => {
    const preRequestValue = isFavourited;

    setFavourite(!preRequestValue);

    try {
      favouriteUploadAPI(upload.imageId).then(() => {});
    } catch (error) {
      setFavourite(preRequestValue);
      toast.error("Unexpected error occurred");
    }
  };

  return (
    <Link to={`${upload.imageId}`}>
      <div className={`upload__card ${error}`}>
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
    </Link>
  );
};

export default Card;
