import React from "react";
import { useNavigate, useRouteError } from "react-router-dom";
import "./ErrorDisplay.css";

interface RouteError {
  data: string;
  error: {
    columnNumber: number;
    fileName: string;
    lineNumber: number;
    message: string;
    stack: string;
  };
  internal: boolean;
  status: number;
  statusText: string;
}

interface Props {}

const ErrorDisplay: React.FC<Props> = () => {
  const navigate = useNavigate();
  const error = useRouteError() as RouteError;

  return (
    <div className="error__content">
      <h1 className="error_title">Oops!</h1>
      <div className="error_message">
        <p>Sorry, an unexpected error has occurred.</p>
        <p>
          <i className="error_message_status">
            {error.status} {error.statusText || error.error.message}
          </i>
        </p>
      </div>
      <button className="error_button btn-primary" onClick={() => navigate(-1)}>
        &larr; Go back
      </button>
    </div>
  );
};

export default ErrorDisplay;
