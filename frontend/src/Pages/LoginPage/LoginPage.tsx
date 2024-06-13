import React from "react";
import "./LoginPage.css";
import AuthForm from "../../Components/Forms/AuthForm/AuthForm.tsx";
import AuthLayout from "../../Components/AuthLayout/AuthLayout.tsx";
import LoginImage from "../../../public/resources/images/Login-Image.jpg";

interface Props {}

const LoginPage: React.FC<Props> = () => {
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
      />
    </AuthLayout>
  );
};

export default LoginPage;
