import React from "react";
import "./InputGroup.css";
import { Field } from "../types";

interface Props {
  field: Field;
  onChange: (id: string, value: string) => void;
}

const InputGroup: React.FC<Props> = ({ field, onChange }) => {
  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    onChange(field.id, e.target.value);
  };

  return (
    <div className="form__input">
      <p className="form__input-title">{field.title}</p>
      <input
        className="form__input-input"
        type={field.type}
        id={field.id}
        placeholder={field.placeholder}
        onChange={handleChange}
      />
    </div>
  );
};

export default InputGroup;
