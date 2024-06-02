import React from "react";
import "./LoginForm.css";
import { FcGoogle } from "react-icons/fc";

interface Props {}

const LoginForm: React.FC<Props> = () => {
  return (
    <div className="login-form">
      <div className="login-form__content">
        <div className="login-form__titles">
          <h1 className="login-form__title">Login</h1>
          <p className="login-form__subtitle">
            Enter your email & password to login to your account
          </p>
        </div>
        <div className="login-form__container">
          <div className="login-form__input">
            <p className="login-form__input-title">Email</p>
            <input
              className="login-form__input-input"
              type="text"
              name=""
              id=""
              placeholder="Enter your email"
            />
          </div>
          <div className="login-form__input">
            <p className="login-form__input-title">Password</p>
            <input
              className="login-form__input-input"
              type="password"
              name=""
              id=""
              placeholder="Enter your password"
            />
          </div>
          <div className="login-form__helpers">
            <div className="login-form__checkbox">
              <input
                type="checkbox"
                id="login-form-remember-me"
                name="remember-me"
              />
              <label htmlFor="login-form-remember-me">Remember me</label>
            </div>
            <p className="login-form__forgot-password">Forgot Password?</p>
          </div>
        </div>
        <div className="login-form__buttons">
          <button className="btn btn-primary login-form__button">Log In</button>
          <div className="divider-container">
            <div className="divider-line"></div>
            <div className="divider-text">OR</div>
            <div className="divider-line"></div>
          </div>
          <button className="btn btn-secondary-curved login-form__button">
            <FcGoogle className="google-icon" />
            Log In with Google
          </button>
        </div>
      </div>
      <div className="login-form__redirect">
        Don't have an account? <a className="signup-redirect-toggle">Sign Up</a>
      </div>
    </div>
  );
};

export default LoginForm;
