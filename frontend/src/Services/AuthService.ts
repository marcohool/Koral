import { axiosInstance } from "./AxiosInstance.ts";

export const login = async (username: string, password: string) => {
  return await axiosInstance.post(`/account/login`, {
    email: username,
    password: password,
  });
};

export const register = async (email: string, password: string) => {
  return await axiosInstance.post(`/account/register`, {
    email: email,
    password: password,
  });
};

export const logout = () => {
  localStorage.removeItem("user");
  localStorage.removeItem("token");
};

export const getCurrentUser = () => {
  return JSON.parse(localStorage.getItem("user") || "{}");
};
