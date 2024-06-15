import React, { useEffect, useState } from "react";
import AuthForm from "../../Components/Forms/AuthForm/AuthForm.tsx";
import AuthLayout from "../../Components/AuthLayout/AuthLayout.tsx";
import RegisterImage from "/resources/images/Signup-Image-Cropped.jpg";
import { ObjectSchema } from "yup";
import {
  FormSchema,
  RegisterFormSchema,
} from "../../Components/Forms/types.ts";
import * as Yup from "yup";
import { useAuth } from "../../Context/useAuth.tsx";
import { toast } from "react-toastify";

interface Props {}

const RegisterPage: React.FC<Props> = () => {
  const [errorMessage, setErrorMessage] = useState("");
  const validationSchema: ObjectSchema<RegisterFormSchema> = Yup.object().shape(
    {
      "form-email": Yup.string()
        .email("Email is not valid")
        .required("Email is required"),
      "form-password-register": Yup.string()
        .min(6, "Password must be at least 6 characters")
        .matches(/\d/, "Password must contain at least one digit")
        .required("Password is required"),
      "form-password-confirm": Yup.string()
        .oneOf([Yup.ref("form-password-register")], "Passwords must match")
        .required("Confirming password is required"),
    },
  );

  const { registerUser, logoutUser } = useAuth();

  useEffect(() => {
    logoutUser();
  });

  const handleFormDisplayError = (formDisplayError: string) => {
    setErrorMessage(formDisplayError);
  };

  const handleRegisterSubmit = (data: FormSchema) => {
    const registerSchema = data as RegisterFormSchema;
    console.log("Register form data submitted:", registerSchema);

    try {
      registerUser(
        registerSchema["form-email"],
        registerSchema["form-password-register"],
        handleFormDisplayError,
      );
    } catch (error) {
      if (error instanceof Error) {
        setErrorMessage(error.message);
      } else {
        toast.error("Unexpected error occurred");
      }
    }
  };

  const clearErrorMessage = () => {
    setErrorMessage("");
  };

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
            id: "form-password-register",
          },
          {
            title: "Confirm Password",
            placeholder: "Confirm your password",
            type: "password",
            id: "form-password-confirm",
          },
        ]}
        displayHelpers={false}
        redirectText="Already have an account?"
        validation={validationSchema}
        handleSubmitInForm={handleRegisterSubmit}
        errorMessage={errorMessage}
        clearErrorMessage={clearErrorMessage}
      />
    </AuthLayout>
  );
};

export default RegisterPage;
