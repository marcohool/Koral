import React from "react";
import FormTitle from "../FormTitle/FormTitle.tsx";
import InputGroup from "../InputGroup/InputGroup.tsx";
import CheckboxGroup from "../../CheckboxGroup/CheckboxGroup.tsx";
import FormHelper from "../FormHelper/FormHelper.tsx";
import FormButtons from "../FormButtons/FormButtons.tsx";
import "./AuthForm.css";
import FormRedirect from "../FormRedirect/FormRedirect.tsx";
import {
  Action,
  Field,
  FormSchema,
  LoginFormSchema,
  RegisterFormSchema,
} from "../types.ts";
import { FormProvider, useForm } from "react-hook-form";
import { ObjectSchema } from "yup";
import { yupResolver } from "@hookform/resolvers/yup";

interface Props {
  action: Action;
  title: string;
  subtitle: string;
  fields: Field[];
  displayHelpers?: boolean;
  redirectText: string;
  validation: ObjectSchema<LoginFormSchema | RegisterFormSchema>;
  handleSubmit: (data: FormSchema) => void;
}

const AuthForm: React.FC<Props> = ({
  action,
  title,
  subtitle,
  fields,
  displayHelpers,
  redirectText,
  validation,
  handleSubmit,
}) => {
  const methods = useForm({ resolver: yupResolver(validation) });

  const onSubmit = methods.handleSubmit((data) => {
    handleSubmit(data);
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
