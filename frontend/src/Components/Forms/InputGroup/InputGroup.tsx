import React from "react";
import "./InputGroup.css";

interface Props {
  title: string;
  placeholder: string;
  type: string;
  id: string;
}

const InputGroup: React.FC<Props> = ({ title, placeholder, type, id }) => {
  return (
    <div className="login-form__input">
      <p className="login-form__input-title">{title}</p>
      <input
        className="login-form__input-input"
        type={type}
        id={id}
        placeholder={placeholder}
      />
    </div>
  );
};

export default InputGroup;
