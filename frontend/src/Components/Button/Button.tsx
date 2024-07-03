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
  isDisabled?: boolean;
}

const Button: FC<ButtonProps> = ({
  value,
  onClick = () => {},
  type,
  children,
  styleOverride,
  className = "",
  isDisabled = false,
}) => {
  const [content, setContent] = useState(value);
  const [customClassName, setCustomClassName] = useState(className);
  const [isDisabledState, setIsDisabledState] = useState(isDisabled);

  useEffect(() => {
    setContent(value);
  }, [value]);

  useEffect(() => {
    setCustomClassName(className);
  }, [className]);

  useEffect(() => {
    setIsDisabledState(isDisabled);
  }, [isDisabled]);

  console.log(isDisabledState);

  return (
    <>
      <button
        className={`button btn-${type} ${customClassName}`}
        onClick={onClick}
        style={styleOverride}
        disabled={isDisabledState}
      >
        {content} {children}
      </button>
    </>
  );
};

export default Button;
