import axios from "axios";

const API_URL = "https://localhost:5001";

export const login = async (username: string, password: string) => {
  return await axios.post(`${API_URL}/account/login`, {
    email: username,
    password: password,
  });
};

export const register = async (email: string, password: string) => {
  return await axios.post(`${API_URL}/account/register`, {
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
