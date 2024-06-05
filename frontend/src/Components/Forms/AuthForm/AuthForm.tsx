import React from "react";
import FormTitle from "../FormTitle/FormTitle.tsx";
import InputGroup from "../InputGroup/InputGroup.tsx";
import CheckboxGroup from "../../CheckboxGroup/CheckboxGroup.tsx";
import Helper from "../../Helpers/Helper.tsx";
import FormButtons from "../FormButtons/FormButtons.tsx";
import "./AuthForm.css";
import FormRedirect from "../FormRedirect/FormRedirect.tsx";

interface Props {}

const AuthForm: React.FC<Props> = () => {
  return (
    <div className="login-form">
      <div className="login-form__content">
        <FormTitle />
        <div className="login-form__input-group">
          <InputGroup />
          <InputGroup />
        </div>
        <div className="login-form__helpers">
          <CheckboxGroup />
          <Helper />
        </div>
        <FormButtons />
      </div>
      <FormRedirect />
    </div>
  );
};

export default AuthForm;
