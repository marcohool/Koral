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
        <FormTitle
          title="Login"
          subtitle="Enter your email & password to login to your account"
        />
        <div className="login-form__input-group">
          <InputGroup
            title="Email"
            placeholder="Enter your email"
            type="text"
            id="login-form-email"
          />
          <InputGroup
            title="Password"
            placeholder="Enter your password"
            type="password"
            id="login-form-password"
          />
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
