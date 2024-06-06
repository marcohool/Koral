import React from "react";
import "./CheckboxGroup.css";

interface Props {}

const CheckboxGroup: React.FC<Props> = () => {
  return (
    <div className="form__checkbox-group">
      <input type="checkbox" id="form-remember-me" name="remember-me" />
      <label className="form__checkbox-label" htmlFor="form-remember-me">
        Remember me
      </label>
    </div>
  );
};

export default CheckboxGroup;
