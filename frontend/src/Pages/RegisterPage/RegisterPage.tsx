import React from "react";
import AuthForm from "../../Components/Forms/AuthForm/AuthForm.tsx";
import AuthLayout from "../../Components/AuthLayout/AuthLayout.tsx";
import RegisterImage from "../../../public/resources/images/Signup-Image-Cropped.jpg";

interface Props {}

const RegisterPage: React.FC<Props> = () => {
  return (
    <AuthLayout formPlacement="right" image={RegisterImage}>
      <AuthForm
        title="Sign Up"
        subtitle="Enter your email & password to create an account"
        action="Sign Up"
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
            id: "form-password",
          },
          {
            title: "Confirm Password",
            placeholder: "Enter your password again",
            type: "password",
            id: "form-password",
          },
        ]}
        redirectText="Already have an account?"
      />
    </AuthLayout>
  );
};

export default RegisterPage;
