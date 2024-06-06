import React from "react";
import "./InputGroup.css";
import { Field } from "../types";

interface Props {
  field: Field;
}

const InputGroup: React.FC<Props> = ({ field }) => {
  return (
    <div className="form__input">
      <p className="form__input-title">{field.title}</p>
      <input
        className="form__input-input"
        type={field.type}
        id={field.id}
        placeholder={field.placeholder}
      />
    </div>
  );
};

export default InputGroup;
