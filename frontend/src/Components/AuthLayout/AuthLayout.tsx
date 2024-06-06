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
            <div className="page__layout-form page__layout-form-left">
              {children}
            </div>
            <div
              className="page__layout-image page__layout-image-right"
              style={{ backgroundImage: `url(${image})` }}
            ></div>
          </>
        ) : (
          <>
            <div
              className="page__layout-image page__layout-image-left"
              style={{ backgroundImage: `url(${image})` }}
            ></div>
            <div className="page__layout-form page__layout-form-right">
              {children}
            </div>
          </>
        )}
      </div>
    </div>
  );
};

export default AuthLayout;
