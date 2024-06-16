import React from "react";
import { FcGoogle } from "react-icons/fc";
import "./FormButtons.css";
import { Action } from "../types.ts";
import Spinner from "../../Spinner/Spinner.tsx";

interface Props {
  action: Action;
  isInternalSubmitSpinnerVisible: boolean;
}

const FormButtons: React.FC<Props> = ({
  action,
  isInternalSubmitSpinnerVisible,
}) => {
  return (
    <div className="form__buttons">
      <button className="btn btn-primary form__button" type="submit">
        {isInternalSubmitSpinnerVisible ? <Spinner /> : action}
      </button>
      <div className="form__divider">
        <div className="divider-line"></div>
        <div className="divider-text">OR</div>
        <div className="divider-line"></div>
      </div>
      <button className="btn btn-secondary form__button">
        <FcGoogle className="form__google-icon" />
        {action} with Google
      </button>
    </div>
  );
};

export default FormButtons;
