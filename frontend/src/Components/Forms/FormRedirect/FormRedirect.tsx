import React from "react";
import { Link } from "react-router-dom";
import "./FormRedirect.css";

interface Props {}

const FormRedirect: React.FC<Props> = () => {
  return (
    <div className="login-form__redirect">
      <p className="login-form__redirect-text">Don't have an account? </p>
      <Link to="/signup">
        <p className="login-form__signup-link">Sign Up</p>
      </Link>
    </div>
  );
};

export default FormRedirect;
