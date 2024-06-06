import React from "react";
import { FcGoogle } from "react-icons/fc";
import "./FormButtons.css";

interface Props {}

const FormButtons: React.FC<Props> = () => {
  return (
    <div className="form__buttons">
      <button className="btn btn-primary form__button">Log In</button>
      <div className="form__divider">
        <div className="divider-line"></div>
        <div className="divider-text">OR</div>
        <div className="divider-line"></div>
      </div>
      <button className="btn btn-secondary form__button">
        <FcGoogle className="form__google-icon" />
        Log In with Google
      </button>
    </div>
  );
};

export default FormButtons;
