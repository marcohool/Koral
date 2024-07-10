import React, { FC, useState } from "react";
import CloseButton from "../../CloseButton/CloseButton.tsx";
import Button from "../../Button/Button.tsx";
import { ButtonType } from "../../Button/types.ts";
import Spinner from "../../Spinner/Spinner.tsx";
import { HelperText, NewUploadFileState } from "../types.ts";
import UploadedImageTile from "./UploadedImageTile.tsx";
import { uploadImageAPI } from "../../../Services/UploadService.ts";
import "./resources/styles/NewUploadDisplay.css";

interface NewUploadDisplayProps {
  onClose: () => void;
  isModal: boolean;
  className?: string;
}

const allowedFileTypes = ["image/jpeg", "image/png"]; // .jpg and .png
const maxFileSize = 10 * 1024 * 1024; // 10MB in bytes

const NewUploadDisplay: FC<NewUploadDisplayProps> = ({
  onClose,
  isModal,
  className,
}) => {
  const [currentFile, setCurrentFile] = useState<File>();
  const [helperText, setHelperText] = useState<HelperText>();
  const [isDragOver, setIsDragOver] = useState<boolean>(false);
  const [uploadProcessing, setUploadProcessing] = useState<boolean>(false);

  const uploadImage = (file: File) => {
    try {
      setUploadProcessing(true);
      const formData = new FormData();
      formData.append("ImageFile", file);

      uploadImageAPI(formData).then(() => {
        setUploadProcessing(false);
      });
    } catch (error) {
      console.error(error);
      setUploadProcessing(false);
    }
  };

  const handleUpload = (file: File) => {
    setCurrentFile(file);

    if (!allowedFileTypes.includes(file.type)) {
      setHelperText({
        message: "Invalid file type. Please upload a .jpg or .png file.",
        type: NewUploadFileState.Invalid,
      });
      return;
    }

    if (file.size > maxFileSize) {
      setHelperText({
        message: "File size exceeds 10MB. Please upload a smaller file.",
        type: NewUploadFileState.Invalid,
      });
      return;
    }

    setHelperText({
      message: "Upload successful",
      type: NewUploadFileState.Valid,
    });

    setTimeout(() => {
      setHelperText(undefined);
    }, 3500);
  };

  const handleDrop = (e: React.DragEvent) => {
    e.preventDefault();
    handleUpload(e.dataTransfer.files[0]);
  };

  const handleFileChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    const uploadedFile = event.target.files ? event.target.files[0] : null;
    if (uploadedFile) {
      handleUpload(uploadedFile);
      event.target.value = "";
    }
  };

  return (
    <div className={`modal ${className}`} onClick={(e) => e.stopPropagation()}>
      <div className="modal-titles">
        <div className="modal-titles-start">
          <h2 className="modal-title">Upload an Image</h2>
          <p className="modal-description">
            Upload the image you want to find the products of
          </p>
        </div>
        <div className="modal-titles-end">
          {isModal && <CloseButton onClick={onClose} size={25} />}
        </div>
      </div>
      <div
        className={`modal__upload-card ${isDragOver ? "drag-over" : ""}`}
        id="drop-zone"
        onDrop={handleDrop}
        onDragOver={(e: React.DragEvent) => {
          e.preventDefault();
          setIsDragOver(true);
        }}
        onDragLeave={(e: React.DragEvent) => {
          e.preventDefault();
          setIsDragOver(false);
        }}
        onClick={() => {
          document.getElementById("fileInput")?.click();
        }}
      >
        <input
          className="modal__upload-card-input"
          type="file"
          id="fileInput"
          accept="image/png, image/jpeg"
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
              className={`modal__upload-card__input__button`}
            />
          </div>
        </div>
      </div>
      <p className="modal__upload-card__helper-text">
        Only .jpg and .png files. Maximum file size of 10MB.
      </p>
      {currentFile && (
        <>
          <div className="modal__uploaded-image-preview">
            <h3 className="modal__uploaded-image-title">Selected Image</h3>
            <UploadedImageTile
              file={currentFile}
              fileState={helperText?.type}
              helper={helperText}
              onDelete={() => {
                setCurrentFile(undefined);
              }}
            />
          </div>
          <div className="modal__upload-image__buttons">
            <Button
              type={ButtonType.secondary}
              value="Cancel"
              onClick={() => {
                setCurrentFile(undefined);
                onClose();
              }}
              styleOverride={{ width: "100%" }}
            />
            <Button
              type={ButtonType.primary}
              value={`${uploadProcessing ? "" : "Upload"}`}
              onClick={() => uploadImage(currentFile)}
              styleOverride={{ width: "100%" }}
              isDisabled={helperText?.type === NewUploadFileState.Invalid}
            >
              {uploadProcessing && (
                <Spinner height="30" colour={"var(--white)"} />
              )}
            </Button>
          </div>
        </>
      )}
    </div>
  );
};

export default NewUploadDisplay;
