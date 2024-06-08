import React, { useState } from "react";
import FormTitle from "../FormTitle/FormTitle.tsx";
import InputGroup from "../InputGroup/InputGroup.tsx";
import CheckboxGroup from "../../CheckboxGroup/CheckboxGroup.tsx";
import FormHelper from "../FormHelper/FormHelper.tsx";
import FormButtons from "../FormButtons/FormButtons.tsx";
import "./AuthForm.css";
import FormRedirect from "../FormRedirect/FormRedirect.tsx";
import { Action, Field } from "../types.ts";

interface Props {
  action: Action;
  title: string;
  subtitle: string;
  fields: Field[];
  displayHelpers?: boolean;
  redirectText: string;
}

const AuthForm: React.FC<Props> = ({
  action,
  title,
  subtitle,
  fields,
  displayHelpers,
  redirectText,
}) => {
  return (
    <form className="form">
      <div className="form__content">
        <FormTitle title={title} subtitle={subtitle} />
        <div className="form__input-group">
          {fields.map((field, index) => (
            <InputGroup key={index} field={field} />
          ))}
        </div>
        {displayHelpers && (
          <div className="form__helpers">
            <CheckboxGroup />
            <FormHelper />
          </div>
        )}
        <FormButtons action={action} />
      </div>
      <FormRedirect action={action} text={redirectText} />
    </form>
  );
};

export default AuthForm;
