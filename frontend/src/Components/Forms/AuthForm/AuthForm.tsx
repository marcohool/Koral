import React, { useEffect } from "react";
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
import ErrorMessage from "../../ErrorAlert/ErrorAlert.tsx";

interface Props {
  action: Action;
  title: string;
  subtitle: string;
  fields: Field[];
  displayHelpers?: boolean;
  redirectText: string;
  validation: ObjectSchema<LoginFormSchema | RegisterFormSchema>;
  handleSubmitInForm: (data: FormSchema) => void;
  errorMessage?: string;
}

const AuthForm: React.FC<Props> = ({
  action,
  title,
  subtitle,
  fields,
  displayHelpers,
  redirectText,
  validation,
  handleSubmitInForm,
  errorMessage,
}) => {
  const methods = useForm({ resolver: yupResolver(validation) });
  const { formState, handleSubmit, reset } = methods;

  const onSubmit = handleSubmit((data) => {
    handleSubmitInForm(data);
  });

  useEffect(() => {
    if (formState.isSubmitSuccessful) {
      reset();
    }
  }, [formState, reset]);

  return (
    <FormProvider {...methods}>
      <form className="form" onSubmit={onSubmit}>
        <div className="form__content">
          <FormTitle title={title} subtitle={subtitle} />
          {errorMessage && <ErrorMessage errorMessage={errorMessage} />}
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
