import React, { FC, useEffect, useState } from "react";
import "./Button.css";
import { ButtonType } from "./types.ts";

interface ButtonProps {
  value?: string;
  onClick?: () => void;
  type: ButtonType;
  children?: React.ReactNode;
  styleOverride?: React.CSSProperties;
  className?: string;
}

const Button: FC<ButtonProps> = ({
  value,
  onClick,
  type,
  children,
  styleOverride,
  className = "",
}) => {
  const [content, setContent] = useState(value);
  const [customClassName, setCustomClassName] = useState(className);

  useEffect(() => {
    setContent(value);
  }, [value]);

  useEffect(() => {
    setCustomClassName(className);
  }, [className]);

  return (
    <>
      <button
        className={`button btn-${type} ${customClassName}`}
        onClick={onClick}
        style={styleOverride}
      >
        {content} {children}
      </button>
    </>
  );
};

export default Button;
