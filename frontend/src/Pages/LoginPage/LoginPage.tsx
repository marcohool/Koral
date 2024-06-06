import React from "react";
import "./LoginPage.css";
import { Link } from "react-router-dom";
import AuthForm from "../../Components/Forms/AuthForm/AuthForm.tsx";

interface Props {}

const LoginPage: React.FC<Props> = () => {
  return (
    <div>
      <div className="page__layout">
        <div className="page__layout-left">
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
                id: "form-password",
              },
            ]}
            redirectText="Don't have an account?"
          />
        </div>
        <div className="page__layout-right">
          <Link to="/" className="page__layout-right-link" />
        </div>
      </div>
    </div>
  );
};

export default LoginPage;
