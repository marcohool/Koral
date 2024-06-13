import axios from "axios";
import { handleError } from "./ErrorHandler.ts";

const API_URL = "https://localhost:64553/api";

export const login = async (username: string, password: string) => {
  try {
    return await axios.post(`${API_URL}/account/login`, {
      username: username,
      password: password,
    });
  } catch (error) {
    handleError(error);
  }
};

export const register = async (
  username: string,
  email: string,
  password: string,
) => {
  try {
    return await axios.post(`${API_URL}/account/register`, {
      username: username,
      email: email,
      password: password,
    });
  } catch (error) {
    handleError(error);
  }
};

export const logout = () => {
  localStorage.removeItem("user");
};

export const getCurrentUser = () => {
  return JSON.parse(localStorage.getItem("user") || "{}");
};
