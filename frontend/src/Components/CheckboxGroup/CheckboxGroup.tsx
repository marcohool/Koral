import React from "react";
import "./CheckboxGroup.css";

interface Props {
  onCheckboxChange: (e: React.ChangeEvent<HTMLInputElement>) => void;
}

const CheckboxGroup: React.FC<Props> = ({ onCheckboxChange }) => {
  return (
    <div className="form__checkbox-group">
      <input
        type="checkbox"
        id="form-remember-me"
        name="remember-me"
        onChange={onCheckboxChange}
      />
      <label className="form__checkbox-label" htmlFor="form-remember-me">
        Remember me
      </label>
    </div>
  );
};

export default CheckboxGroup;
