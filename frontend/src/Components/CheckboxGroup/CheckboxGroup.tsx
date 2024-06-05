import React from "react";
import "./CheckboxGroup.css";

interface Props {}

const CheckboxGroup: React.FC<Props> = () => {
  return (
    <div className="login-form__checkbox-group">
      <input type="checkbox" id="login-form-remember-me" name="remember-me" />
      <label
        className="login-form__checkbox-label"
        htmlFor="login-form-remember-me"
      >
        Remember me
      </label>
    </div>
  );
};

export default CheckboxGroup;
