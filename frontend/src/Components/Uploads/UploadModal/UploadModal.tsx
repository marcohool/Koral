import React, { FC, useState } from "react";
import "./UploadModal.css";
import { GoFileMedia, GoX } from "react-icons/go";
import Button from "../../Button/Button.tsx";
import { ButtonType } from "../../Button/types.ts";
import {
  MdAddPhotoAlternate,
  MdOutlineAddPhotoAlternate,
  MdOutlineInsertPhoto,
} from "react-icons/md";

interface UploadModalProps {
  onClose: () => void;
}

const UploadModal: FC<UploadModalProps> = ({ onClose }) => {
  const [file, setFile] = useState<File | null>(null);

  const handleContentClick = (e: React.MouseEvent) => {
    e.stopPropagation();
  };

  const handleUpload = () => {
    document.getElementById("fileInput")?.click();
  };

  const handleDrop = (e: React.DragEvent) => {
    e.preventDefault();
    const dt = e.dataTransfer;
    const files = dt.files;
    setFile(files[0]);
    console.log(files);
  };

  const handleDragOver = (e: React.DragEvent) => {
    e.preventDefault();
  };

  return (
    <div className="modal-backdrop" onClick={onClose}>
      <div className="modal" onClick={handleContentClick}>
        <div className="modal-titles">
          <div className="modal-titles-start">
            <h2 className="modal-title">Upload an Image</h2>
            <p className="modal-description">
              Upload the image you want to find the products of
            </p>
          </div>
          <div className="modal-titles-end">
            <div className="modal-close" onClick={onClose}>
              <GoX size={25} />
            </div>
          </div>
        </div>
        <div
          className="modal__upload-card"
          id="drop-zone"
          onDrop={handleDrop}
          onDragOver={handleDragOver}
          onClick={handleUpload}
        >
          <input
            className="modal__upload-card-input"
            type="file"
            id="fileInput"
          />
          <div className="modal__upload-card__input__content">
            <h2 className="modal__upload-card__input__title">
              Drag & drop your file here or{" "}
            </h2>
            <div className="modal__upload-card__input__button_wrapper">
              <Button
                type={ButtonType.primary}
                value="Choose File"
                className="modal__upload-card__input__button"
              />
            </div>
          </div>
        </div>
        <p className="modal__upload-card__helper-text">
          Only .jpg and .png files. Maximum file size of 10MB.
        </p>
        {file && (
          <div className="modal__uploaded-image-preview">
            <h3 className="modal__uploaded-image-title">Uploaded Image</h3>
          </div>
        )}
      </div>
    </div>
  );
};

export default UploadModal;
