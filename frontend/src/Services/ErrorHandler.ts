import { AxiosError } from "axios";
import { toast } from "react-toastify";

/**
 * Handles errors from Axios requests
 * @param error
 * @param setFormDisplayErrorMessage
 * @deprecated
 */
export const handleError = (
  error: unknown,
  setFormDisplayErrorMessage?: (errorMessage: string) => void,
) => {
  if (error instanceof AxiosError) {
    const message = error.response?.data || "Server Unavailable";
    const code = error.response?.status || 503;

    if (code === 400) {
      setFormDisplayErrorMessage
        ? setFormDisplayErrorMessage(message)
        : toast.error(message);
    } else if (code === 401) {
      toast.error("Unauthorized");
      window.history.pushState({}, "LoginPage", "/login");
    } else {
      toast.error(message);
    }
  } else {
    toast.error("Unknown error has occurred");
    console.error(error);
  }
};

export const handleErrorV2 = (error: unknown) => {
  if (error instanceof AxiosError) {
    const message = error.message || "Server Unavailable";
    const code = error.response?.status || 503;

    if (code === 400) {
      return message;
    } else if (code === 401) {
      return "Unauthorized";
    } else {
      return message;
    }
  } else {
    return "Unknown error has occurred";
  }
};
