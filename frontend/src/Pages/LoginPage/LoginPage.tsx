import React from "react";
import "./LoginPage.css";
import AuthForm from "../../Components/Forms/AuthForm/AuthForm.tsx";
import AuthLayout from "../../Components/AuthLayout/AuthLayout.tsx";
import LoginImage from "../../../public/resources/images/Login-Image.jpg";
import * as Yup from "yup";
import { ObjectSchema } from "yup";
import { LoginFormSchema } from "../../Components/Forms/types.ts";

interface Props {}

const LoginPage: React.FC<Props> = () => {
  const validationSchema: ObjectSchema<LoginFormSchema> = Yup.object().shape({
    "form-email": Yup.string()
      .email("Email is not valid")
      .required("Email is required"),
    "form-password-login": Yup.string().required("Password is required"),
  });

  return (
    <AuthLayout formPlacement="left" image={LoginImage}>
      <AuthForm
        action="Log In"
        title="Login"
        subtitle="Enter your email & password to login to your account"
        fields={[
          {
            title: "Email",
            placeholder: "Enter your email",
            type: "text",
            id: "form-email",
          },
          {
            title: "Password",
            placeholder: "Enter your password",
            type: "password",
            id: "form-password-login",
          },
        ]}
        displayHelpers={true}
        redirectText="Don't have an account?"
        validation={validationSchema}
      />
    </AuthLayout>
  );
};

export default LoginPage;
