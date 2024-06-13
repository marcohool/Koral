import React from "react";
import "./InputGroup.css";
import { Field } from "../types";
import { useFormContext } from "react-hook-form";

interface Props {
  field: Field;
}

const InputGroup: React.FC<Props> = ({ field }) => {
  const {
    register,
    formState: { errors },
  } = useFormContext();

  const errorMessage = errors[field.id]?.message as string | undefined;

  return (
    <div className="form__input">
      <p className={`form__input-title ${errorMessage && "error-state"}`}>
        {field.title}
      </p>
      <input
        className={`form__input-input ${errorMessage && "form__input-error-state"}`}
        type={field.type}
        id={field.id}
        placeholder={field.placeholder}
        {...register(field.id, {
          required: {
            value: true,
            message: "This field is required",
          },
        })}
      />
      {errorMessage && <span className="error-message">{errorMessage}</span>}
    </div>
  );
};

export default InputGroup;
