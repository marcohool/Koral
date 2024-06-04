import React from "react";
import "./LoginForm.css";
import { FcGoogle } from "react-icons/fc";
import { Link } from "react-router-dom";

interface Props {}

const LoginForm: React.FC<Props> = () => {
  return (
    <div className="login-form">
      <div className="login-form__content">
        <div className="login-form__titles">
          <h2 className="login-form__title">Login</h2>
          <p className="login-form__subtitle">
            Enter your email & password to login to your account
          </p>
        </div>
        <div className="login-form__container">
          <div className="login-form__input-group">
            <p className="login-form__input-title">Email</p>
            <input
              className="login-form__input-input"
              type="text"
              name=""
              id="login-form-email"
              placeholder="Enter your email"
            />
          </div>
          <div className="login-form__input">
            <p className="login-form__input-title">Password</p>
            <input
              className="login-form__input-input"
              type="password"
              name=""
              id="login-form-password"
              placeholder="Enter your password"
            />
          </div>
          <div className="login-form__helpers">
            <div className="login-form__checkbox-group">
              <input
                type="checkbox"
                id="login-form-remember-me"
                name="remember-me"
              />
              <label
                className="login-form__checkbox-label"
                htmlFor="login-form-remember-me"
              >
                Remember me
              </label>
            </div>
            <p className="login-form__forgot-password">Forgot Password?</p>
          </div>
        </div>
        <div className="login-form__buttons">
          <button className="btn btn-primary login-form__button">Log In</button>
          <div className="login-form__divider">
            <div className="divider-line"></div>
            <div className="divider-text">OR</div>
            <div className="divider-line"></div>
          </div>
          <button className="btn btn-secondary login-form__button">
            <FcGoogle className="login-form__google-icon" />
            Log In with Google
          </button>
        </div>
      </div>
      <div className="login-form__redirect">
        <p className="login-form__redirect-text">Don't have an account? </p>
        <Link to="/signup">
          <p className="login-form__signup-link">Sign Up</p>
        </Link>
      </div>
    </div>
  );
};

export default LoginForm;
