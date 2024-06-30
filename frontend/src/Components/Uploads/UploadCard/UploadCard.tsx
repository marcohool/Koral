import React from "react";
import "./resources/styles/UploadCard.css";
import { StatusType, Upload } from "../UploadsType.ts";
import { parseDate } from "../helpers.ts";
import { RiTShirt2Line } from "react-icons/ri";
import UploadRating from "./UploadRating.tsx";

const API_URL = "https://localhost:5001/";

interface Props {
  upload: Upload;
}

const UploadCard: React.FC<Props> = ({ upload }) => {
  const status = StatusType[upload.status];
  const error = upload.status === StatusType.Failed ? "error" : undefined;
  const processing =
    upload.status === StatusType.Processing ? "processing" : "";
  const isProcessed = upload.status === StatusType.Processed;

  return (
    <div className={`upload__card ${error} `}>
      <div className="upload__card-image">
        <img src={API_URL + upload.imagePath} alt={upload.imageId} />
      </div>
      <div className={"upload__card__content"}>
        <div className="upload__card-titles">
          <h3 className="upload__card-titles-title">
            Placeholder Image Title Value
          </h3>
          <div className={`upload__card-titles-status`}>
            {upload.clothingItemsMatched} <RiTShirt2Line />
          </div>
        </div>
        <div className="upload__card-details">
          <p className={`upload__card-details-status ${processing}`}>
            {isProcessed ? (
              <>
                <UploadRating rating={upload.accuracyRating} />{" "}
              </>
            ) : (
              status
            )}
          </p>
          <p className="upload__card-details-date ">
            {parseDate(upload.createdAt)}
          </p>
        </div>
      </div>
    </div>
  );
};

export default UploadCard;
