import axios, { AxiosError } from "axios";
import { toast } from "react-toastify";

const API_URL = "https://localhost:5001";

export const axiosInstance = axios.create({
  baseURL: API_URL,
  timeout: 10000,
  headers: {
    "Content-type": "application/json",
  },
});

const errorHandler = (error: unknown) => {
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

  return Promise.reject(error);
};
axiosInstance.interceptors.response.use(
  (response) => {
    return response;
  },
  (error) => errorHandler(error),
);
