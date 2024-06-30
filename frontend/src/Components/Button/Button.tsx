import React, { FC, useState } from "react";
import "./Button.css";
import { ButtonType } from "./types.ts";

interface ButtonProps {
  value?: string;
  onClick?: () => void;
  type: ButtonType;
  children?: React.ReactNode;
  styleOverride?: React.CSSProperties;
}

const Button: FC<ButtonProps> = ({
  value,
  onClick,
  type,
  children,
  styleOverride,
}) => {
  const [content] = useState(value);

  return (
    <>
      <button
        className={`button btn-${type}`}
        onClick={onClick}
        style={styleOverride}
      >
        {content} {children}
      </button>
    </>
  );
};

export default Button;
