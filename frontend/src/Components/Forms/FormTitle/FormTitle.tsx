import React from "react";
import "./FormTitle.css";

interface Props {}

const FormTitle: React.FC<Props> = () => {
  return (
    <div className="login-form__titles">
      <h2 className="login-form__title">Login</h2>
      <p className="login-form__subtitle">
        Enter your email & password to login to your account
      </p>
    </div>
  );
};

export default FormTitle;
