import React from "react";
import LoginForm from "../../Components/LoginForm/LoginForm.tsx";
import "./LoginPage.css";

interface Props {}

const LoginPage: React.FC<Props> = () => {
  return (
    <div>
      <div className="page__layout">
        <LoginForm />
      </div>
    </div>
  );
};

export default LoginPage;
