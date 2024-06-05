import React from "react";
import { FcGoogle } from "react-icons/fc";
import "./FormButtons.css";

interface Props {}

const FormButtons: React.FC<Props> = () => {
  return (
    <div className="login-form__buttons">
      <button className="btn btn-primary login-form__button">Log In</button>
      <div className="login-form__divider">
        <div className="divider-line"></div>
        <div className="divider-text">OR</div>
        <div className="divider-line"></div>
      </div>
      <button className="btn btn-secondary login-form__button">
        <FcGoogle className="login-form__google-icon" />
        Log In with Google
      </button>
    </div>
  );
};

export default FormButtons;
