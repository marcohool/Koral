import React, { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import axios from "axios";
import { login, logout, register } from "../Services/AuthService.ts";
import { handleError } from "../Services/ErrorHandler.ts";

type UserContextType = {
  user: string | null;
  token: string | null;
  registerUser: (
    email: string,
    password: string,
    setFormDisplayErrorMessage: (errorMessage: string) => void,
  ) => void;
  loginUser: (
    email: string,
    password: string,
    setFormDisplayErrorMessage: (errorMessage: string) => void,
  ) => void;
  logoutUser: () => void;
  isLoggedIn: () => boolean;
};

type Props = { children: React.ReactNode };

const UserContext = React.createContext<UserContextType>({} as UserContextType);

export const UserProvider = ({ children }: Props) => {
  const navigate = useNavigate();
  const [token, setToken] = useState<string | null>(null);
  const [user, setUser] = useState<string | null>(null);
  const [isReady, setIsReady] = useState(false);

  useEffect(() => {
    const user = localStorage.getItem("user");
    const token = localStorage.getItem("token");
    if (user && token) {
      setUser(user);
      setToken(token);
      axios.defaults.headers.common["Authorization"] = `Bearer ${token}`;
    }
    setIsReady(true);
  }, []);

  const registerUser = async (
    email: string,
    password: string,
    setErrorMessage: (errorMessage: string) => void,
  ) => {
    await register(email, password).catch((error) =>
      handleError(error, setErrorMessage),
    );
  };

  const loginUser = async (
    email: string,
    password: string,
    setErrorMessage: (errorMessage: string) => void,
  ) => {
    await login(email, password)
      .then((response) => {
        if (response) {
          localStorage.setItem("token", response.data);
          localStorage.setItem("user", email);

          setToken(response?.data.token);
          setUser(email);

          navigate("/");
        }
      })
      .catch((error) => handleError(error, setErrorMessage));
  };

  const isLoggedIn = () => {
    return !!user;
  };

  const logoutUser = () => {
    logout();
    setUser(null);
    setToken(null);
  };

  return (
    <UserContext.Provider
      value={{
        user,
        token,
        registerUser,
        loginUser,
        isLoggedIn,
        logoutUser,
      }}
    >
      {isReady && children}
    </UserContext.Provider>
  );
};

export const useAuth = () => React.useContext(UserContext);
