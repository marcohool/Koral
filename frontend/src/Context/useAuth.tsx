import React, { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import axios from "axios";
import { login, register } from "../Services/AuthService.ts";

type UserContextType = {
  user: string | null;
  token: string | null;
  registerUser: (email: string, password: string) => void;
  loginUser: (email: string, password: string) => void;
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

  const registerUser = async (email: string, password: string) => {
    await register(email, password);
  };

  const loginUser = async (email: string, password: string) => {
    await login(email, password)
      .then((response) => {
        if (response) {
          localStorage.setItem("token", response.data);
          localStorage.setItem("user", email);

          setToken(response?.data.token);
          setUser(email);

          console.log("User registered successfully", token, email);

          navigate("/");
        }
      })
      .catch((error) => console.log("Server error occurred " + error));
  };

  const isLoggedIn = () => {
    return !!user;
  };

  const logoutUser = () => {
    localStorage.removeItem("user");
    localStorage.removeItem("token");
    setUser(null);
    setToken(null);
    navigate("/");
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
