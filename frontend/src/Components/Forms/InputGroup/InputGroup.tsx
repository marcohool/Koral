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
    <div className="form__input">
      <p className="form__input-title">{title}</p>
      <input
        className="form__input-input"
        type={type}
        id={id}
        placeholder={placeholder}
      />
    </div>
  );
};

export default InputGroup;
