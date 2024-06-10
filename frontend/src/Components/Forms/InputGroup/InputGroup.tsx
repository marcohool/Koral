import React from "react";
import "./InputGroup.css";
import { Field } from "../types";
import { useFormContext } from "react-hook-form";

interface Props {
  field: Field;
}

const InputGroup: React.FC<Props> = ({ field }) => {
  const { register } = useFormContext();

  return (
    <div className="form__input">
      <p className="form__input-title">{field.title}</p>
      <input
        className="form__input-input"
        type={field.type}
        id={field.id}
        placeholder={field.placeholder}
        // onChange={handleChange}
        {...register(field.id, {
          required: {
            value: true,
            message: "required",
          },
        })}
      />
    </div>
  );
};

export default InputGroup;
