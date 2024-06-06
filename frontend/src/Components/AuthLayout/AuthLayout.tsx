import { Link } from "react-router-dom";
import React from "react";
import "./AuthLayout.css";

interface AuthLayoutProps {
  children: React.ReactNode;
  formPlacement: "left" | "right";
  image: string;
}

const AuthLayout: React.FC<AuthLayoutProps> = ({
  children,
  formPlacement,
  image,
}) => {
  return (
    <div>
      <div className="page__layout">
        {formPlacement === "left" ? (
          <>
            <div className="page__layout-form">{children}</div>
            <div
              className="page__layout-image"
              style={{ backgroundImage: `url(${image})` }}
            >
              <Link to="/" className="page__layout-image-link" />
            </div>
          </>
        ) : (
          <>
            <div
              className="page__layout-image"
              style={{ backgroundImage: `url(${image})` }}
            >
              <Link to="/" className="page__layout-image-link" />
            </div>
            <div className="page__layout-form">{children}</div>
          </>
        )}
      </div>
    </div>
  );
};

export default AuthLayout;
