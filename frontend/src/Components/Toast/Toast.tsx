import React from "react";
import { CustomSlide } from "../../Utils/Toast/toastConfig.ts";
import { ToastContainer } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";
import "./Toast.css";

interface Props {}

const Toast: React.FC<Props> = () => {
  return (
    <ToastContainer
      position="top-right"
      autoClose={4500}
      closeButton={true}
      hideProgressBar={true}
      newestOnTop={false}
      closeOnClick
      rtl={false}
      pauseOnFocusLoss={false}
      draggable={false}
      theme="light"
      transition={CustomSlide}
      limit={4}
    />
  );
};

export default Toast;
