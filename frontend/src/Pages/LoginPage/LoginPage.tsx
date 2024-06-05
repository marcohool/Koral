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
          <AuthForm />
        </div>
        <div className="page__layout-right">
          <Link to="/" className="page__layout-right-link" />
        </div>
      </div>
    </div>
  );
};

export default LoginPage;
