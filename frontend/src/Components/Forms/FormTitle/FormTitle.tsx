import React from "react";
import "./FormTitle.css";

interface Props {
  title: string;
  subtitle: string;
}

const FormTitle: React.FC<Props> = ({ title, subtitle }) => {
  return (
    <div className="form__titles">
      <h2 className="form__title">{title}</h2>
      <p className="form__subtitle">{subtitle}</p>
    </div>
  );
};

export default FormTitle;
