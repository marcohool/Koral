import { FC } from "react";
import { TbX } from "react-icons/tb";
import "./CloseButton.css";

interface CloseButtonProps {
  onClick?: () => void;
  size?: number;
}

const CloseButton: FC<CloseButtonProps> = ({ onClick, size }) => {
  return (
    <div className="close-button__button" onClick={onClick}>
      <TbX size={size} />
    </div>
  );
};

export default CloseButton;
