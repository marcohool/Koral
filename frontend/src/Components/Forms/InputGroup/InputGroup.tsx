import React from "react";
import "./InputGroup.css";

interface Props {}

const InputGroup: React.FC<Props> = () => {
  return (
    <div className="login-form__input">
      <p className="login-form__input-title">Email</p>
      <input
        className="login-form__input-input"
        type="text"
        name=""
        id="login-form-email"
        placeholder="Enter your email"
      />
    </div>
  );
};

export default InputGroup;
