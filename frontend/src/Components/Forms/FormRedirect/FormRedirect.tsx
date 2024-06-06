import React from "react";
import { Link } from "react-router-dom";
import "./FormRedirect.css";
import { Action } from "../types.ts";

interface Props {
  action: Action;
  text: string;
}

const FormRedirect: React.FC<Props> = ({ action, text }) => {
  return (
    <div className="form__redirect">
      <p className="form__redirect-text">{text}</p>
      <Link to={action == "Log In" ? "/signup" : "/login"}>
        <p className="form__signup-link">
          {action == "Log In" ? "Sign Up" : "Log In"}
        </p>
      </Link>
    </div>
  );
};

export default FormRedirect;
