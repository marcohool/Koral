import { AxiosError } from "axios";
import { toast } from "react-toastify";

export const handleError = (error: unknown) => {
  if (error instanceof AxiosError) {
    const message = error.response?.data || "Server Unavailable";
    const code = error.response?.status || 503;

    if (code === 401) {
      toast.error("Unauthorized");
      window.history.pushState({}, "LoginPage", "/login");
    } else {
      toast.error(message);
    }
  } else {
    toast.error("Unknown error has occurred");
    console.log(error);
  }
};
