import React from "react";
import "./CheckboxGroup.css";
import { useFormContext } from "react-hook-form";

interface Props {}

const CheckboxGroup: React.FC<Props> = () => {
  const { register } = useFormContext();

  return (
    <div className="form__checkbox-group">
      <input
        type="checkbox"
        id="form-remember-me"
        {...register("form-remember-me", {})}
      />
      <label className="form__checkbox-label" htmlFor="form-remember-me">
        Remember me
      </label>
    </div>
  );
};

export default CheckboxGroup;
