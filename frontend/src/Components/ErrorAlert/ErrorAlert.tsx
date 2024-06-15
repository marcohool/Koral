import React from "react";
import "./ErrorAlert.css";
import { ErrorCloseIcon, ErrorIcon } from "./ErrorIcons.tsx";

interface Props {
  errorMessage: string;
  isVisible: boolean;
  onClose: () => void;
}

const ErrorAlert: React.FC<Props> = ({ errorMessage, isVisible, onClose }) => {
  if (!isVisible) {
    return null;
  }

  return (
    <div className="alert__container">
      <div className="alert__body">
        <div className="alert__icon">
          <ErrorIcon />
        </div>
        <p className="alert__text">{errorMessage}</p>
      </div>
      <div className="alert__close" onClick={onClose}>
        <ErrorCloseIcon />
      </div>
    </div>
  );
};

export default ErrorAlert;
