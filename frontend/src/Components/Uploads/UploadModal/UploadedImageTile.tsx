import { FC, useEffect, useState } from "react";
import "./resources/styles/UploadedImageTile.css";
import { TbPhotoFilled } from "react-icons/tb";
import CloseButton from "../../CloseButton/CloseButton.tsx";
import { bytesToMB } from "../../../helpers/fileHelpers.ts";

interface UploadedImageTileProps {
  file: File;
  isSuccess?: boolean;
  onDelete: () => void;
  errorMessage?: string;
}

const UploadedImageTile: FC<UploadedImageTileProps> = ({
  file,
  onDelete,
  isSuccess = false,
  errorMessage,
}) => {
  const [isSuccessState, setIsSuccessState] = useState(isSuccess);
  const [error, setError] = useState(errorMessage);

  useEffect(() => {
    setIsSuccessState(isSuccess);
  }, [isSuccess]);

  useEffect(() => {
    if (errorMessage) {
      setError(errorMessage);
      return;
    }
    setError("");
  }, [errorMessage]);

  return (
    <>
      <div
        className={`uploaded-image__tile ${isSuccessState ? "tile-success" : ""} ${error ? "tile-error" : ""}`}
      >
        <div className="uploaded-image__start">
          <div className="uploaded-image__tile__icon-image">
            <TbPhotoFilled />
          </div>
          <div className="uploaded-image__text">
            <div className="uploaded-image__name">{file.name}</div>
            <div className="uploaded-image__size">{bytesToMB(file.size)}</div>
          </div>
        </div>
        <div className="uploaded-image__end">
          <CloseButton onClick={onDelete} />
        </div>
      </div>
      {error && <p className="uploaded-image__error">{error}</p>}
    </>
  );
};

export default UploadedImageTile;
