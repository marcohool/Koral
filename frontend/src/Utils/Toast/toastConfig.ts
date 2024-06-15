import { cssTransition } from "react-toastify";
import "./toastStyle.css";

export const CustomSlide = cssTransition({
  enter: "slide-in-right",
  exit: "slide-out-right",
});
