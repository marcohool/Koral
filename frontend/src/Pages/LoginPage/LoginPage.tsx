import React from "react";
import LoginForm from "../../Components/LoginForm/LoginForm.tsx";
import "./LoginPage.css";
import { Link } from "react-router-dom";

interface Props {}

const LoginPage: React.FC<Props> = () => {
  return (
    <div>
      <div className="page__layout">
        <div className="page__layout-left">
          <LoginForm />
        </div>
        <div className="page__layout-right">
          <Link to="/" className="page__layout-right-link" />
        </div>
      </div>
    </div>
  );
};

export default LoginPage;
