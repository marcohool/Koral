import React, { FC, useState } from "react";
import "./resources/styles/UploadModal.css";
import Button from "../../Button/Button.tsx";
import { ButtonType } from "../../Button/types.ts";
import UploadedImageTile from "./UploadedImageTile.tsx";
import CloseButton from "../../CloseButton/CloseButton.tsx";

interface UploadModalProps {
  onClose: () => void;
}

const UploadModal: FC<UploadModalProps> = ({ onClose }) => {
  const [file, setFile] = useState<File | null>(null);
  const [uploadSuccess, setUploadSuccess] = useState<boolean>(false);

  const handleContentClick = (e: React.MouseEvent) => {
    e.stopPropagation();
  };

  const handleUpload = (file: File) => {
    setFile(file);
    setUploadSuccess(true);

    setTimeout(() => {
      setUploadSuccess(false);
    }, 3500);
  };

  const clickUpload = () => {
    document.getElementById("fileInput")?.click();
  };

  const handleFileChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    const uploadedFile = event.target.files ? event.target.files[0] : null;
    if (uploadedFile) {
      handleUpload(uploadedFile);
      event.target.value = "";
    }
  };

  const handleDrop = (e: React.DragEvent) => {
    e.preventDefault();
    const dt = e.dataTransfer;
    const files = dt.files;
    handleUpload(files[0]);
  };

  const handleDragOver = (e: React.DragEvent) => {
    e.preventDefault();
  };

  const handleDelete = () => {
    setFile(null);
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
            <CloseButton onClick={onClose} size={25} />
          </div>
        </div>
        <div
          className="modal__upload-card"
          id="drop-zone"
          onDrop={handleDrop}
          onDragOver={handleDragOver}
          onClick={clickUpload}
        >
          <input
            className="modal__upload-card-input"
            type="file"
            id="fileInput"
            onChange={handleFileChange}
          />
          <div className="modal__upload-card__input__content">
            <h2 className="modal__upload-card__input__title">
              Drag & drop your file here, or
            </h2>
            <div className="modal__upload-card__input__button_wrapper">
              <Button
                type={ButtonType.primary}
                value="Choose File"
                className={`modal__upload-card__input__button ${uploadSuccess ? "success" : ""}`}
              />
            </div>
          </div>
        </div>
        <p className="modal__upload-card__helper-text">
          Only .jpg and .png files. Maximum file size of 10MB.
        </p>
        {file && (
          <>
            <div className="modal__uploaded-image-preview">
              <h3 className="modal__uploaded-image-title">Uploaded Image</h3>
              <UploadedImageTile
                file={file}
                onDelete={handleDelete}
                isSuccess={uploadSuccess}
              />
            </div>
            <div className="modal__upload-image__buttons">
              <Button
                type={ButtonType.secondary}
                value="Cancel"
                onClick={onClose}
                styleOverride={{ width: "100%" }}
              />
              <Button
                type={ButtonType.primary}
                value="Upload"
                onClick={() => console.log("Upload")}
                styleOverride={{ width: "100%" }}
              />
            </div>
          </>
        )}
      </div>
    </div>
  );
};

export default UploadModal;
