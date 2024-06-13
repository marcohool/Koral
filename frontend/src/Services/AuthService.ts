import axios from "axios";

const API_URL = "https://localhost:5050/auth";

export const register = (email: string, password: string) => {
  return axios.post(`${API_URL}/register`, {
    email,
    password,
  });
};

export const login = (email: string, password: string) => {
  return axios
    .post(`${API_URL}/login`, {
      email,
      password,
    })
    .then((response) => {
      if (response.data.accessToken) {
        localStorage.setItem("user", JSON.stringify(response.data));
      }

      return response.data;
    });
};

export const logout = () => {
  localStorage.removeItem("user");
};

export const getCurrentUser = () => {
  return JSON.parse(localStorage.getItem("user") || "{}");
};
