import React from "react";
import FormTitle from "../FormTitle/FormTitle.tsx";
import InputGroup from "../InputGroup/InputGroup.tsx";
import CheckboxGroup from "../../CheckboxGroup/CheckboxGroup.tsx";
import FormHelper from "../FormHelper/FormHelper.tsx";
import FormButtons from "../FormButtons/FormButtons.tsx";
import "./AuthForm.css";
import FormRedirect from "../FormRedirect/FormRedirect.tsx";

interface Props {}

const AuthForm: React.FC<Props> = () => {
  return (
    <div className="form">
      <div className="form__content">
        <FormTitle
          title="Login"
          subtitle="Enter your email & password to login to your account"
        />
        <div className="form__input-group">
          <InputGroup
            title="Email"
            placeholder="Enter your email"
            type="text"
            id="form-email"
          />
          <InputGroup
            title="Password"
            placeholder="Enter your password"
            type="password"
            id="form-password"
          />
        </div>
        <div className="form__helpers">
          <CheckboxGroup />
          <FormHelper />
        </div>
        <FormButtons />
      </div>
      <FormRedirect />
    </div>
  );
};

export default AuthForm;
