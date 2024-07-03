import { FC } from "react";
import { HelperText, NewUploadFileState } from "../types.ts";
import { TbPhotoFilled } from "react-icons/tb";
import { bytesToMB } from "../../../helpers/fileHelpers.ts";
import CloseButton from "../../CloseButton/CloseButton.tsx";
import "./resources/styles/UploadedImageTile.css";

interface UploadedImageTileProps {
  file: File;
  fileState?: NewUploadFileState;
  helper?: HelperText;
  onDelete: () => void;
}

const UploadedImageTile: FC<UploadedImageTileProps> = ({
  file,
  fileState,
  helper,
  onDelete,
}) => {
  return (
    <>
      <div
        className={`uploaded-image__tile ${fileState === NewUploadFileState.Valid ? "tile-success" : ""} 
        ${fileState === NewUploadFileState.Invalid ? "tile-error" : ""}`}
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
      <div
        className={`upload-image__helper-text ${helper?.type === NewUploadFileState.Valid ? "success" : ""} 
        ${helper?.type === NewUploadFileState.Invalid ? "error" : ""}`}
      >
        {helper?.message}
      </div>
    </>
  );
};

export default UploadedImageTile;
