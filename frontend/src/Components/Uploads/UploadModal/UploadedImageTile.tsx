import { FC } from "react";
import "./resources/styles/UploadedImageTile.css";
import { TbPhotoFilled } from "react-icons/tb";
import CloseButton from "../../CloseButton/CloseButton.tsx";

interface UploadedImageTileProps {
  file: File;
  onDelete: () => void;
}

const UploadedImageTile: FC<UploadedImageTileProps> = ({ file, onDelete }) => {
  const bytesToMB = (bytes: number, decimals: number = 2) => {
    if (bytes === 0) return "0 MB";
    const k = 1024;
    return (bytes / (k * k)).toFixed(decimals) + " MB";
  };

  return (
    <div className="uploaded-image__tile">
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
  );
};

export default UploadedImageTile;
