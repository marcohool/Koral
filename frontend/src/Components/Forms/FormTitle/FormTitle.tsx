import React from "react";
import "./FormTitle.css";

interface Props {
  title: string;
  subtitle: string;
}

const FormTitle: React.FC<Props> = ({ title, subtitle }) => {
  return (
    <div className="login-form__titles">
      <h2 className="login-form__title">{title}</h2>
      <p className="login-form__subtitle">{subtitle}</p>
    </div>
  );
};

export default FormTitle;
