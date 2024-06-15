import React from "react";
import "./ErrorAlert.css";
import ErrorIcon from "./ErrorIcon.tsx";

interface Props {
  errorMessage: string;
}

const ErrorAlert: React.FC<Props> = ({ errorMessage }) => {
  return (
    <div className="alert__container">
      <div className="alert__body">
        <div className="alert__icon">
          <ErrorIcon />
        </div>
        <p className="alert__text">{errorMessage}</p>
      </div>
    </div>
  );
};

export default ErrorAlert;
