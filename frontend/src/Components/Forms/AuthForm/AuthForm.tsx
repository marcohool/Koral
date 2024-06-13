import React from "react";
import FormTitle from "../FormTitle/FormTitle.tsx";
import InputGroup from "../InputGroup/InputGroup.tsx";
import CheckboxGroup from "../../CheckboxGroup/CheckboxGroup.tsx";
import FormHelper from "../FormHelper/FormHelper.tsx";
import FormButtons from "../FormButtons/FormButtons.tsx";
import "./AuthForm.css";
import FormRedirect from "../FormRedirect/FormRedirect.tsx";
import { Action, Field } from "../types.ts";
import { FormProvider, useForm } from "react-hook-form";
import * as Yup from "yup";
import { yupResolver } from "@hookform/resolvers/yup";

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
  const validationSchema = Yup.object().shape({
    "form-email": Yup.string()
      .email("Email is not valid")
      .required("Email is required"),
    "form-password-register": Yup.string()
      .min(6, "Password must be at least 6 characters")
      .matches(/\d/, "Password must contain at least one digit")
      .required("Password is required"),
    "form-password-login": Yup.string().required("Password is required"),
    "form-password-confirm": Yup.string()
      .oneOf([Yup.ref("form-password")], "Passwords must match")
      .required("Confirming password is required"),
  });

  const methods = useForm({ resolver: yupResolver(validationSchema) });

  const onSubmit = methods.handleSubmit((data) => {
    console.log(data);
  });

  return (
    <FormProvider {...methods}>
      <form className="form" onSubmit={onSubmit}>
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
    </FormProvider>
  );
};

export default AuthForm;
