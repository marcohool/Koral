import { createBrowserRouter } from "react-router-dom";
import App from "../App.tsx";
import HomePage from "../Pages/HomePage/HomePage.tsx";
import ErrorPage from "../Pages/ErrorPage/ErrorPage.tsx";
import RegisterPage from "../Pages/RegisterPage/RegisterPage.tsx";
import LoginPage from "../Pages/LoginPage/LoginPage.tsx";

export const router = createBrowserRouter([
  {
    path: "/",
    element: <App />,
    children: [
      { path: "", element: <HomePage /> },
      { path: "login", element: <LoginPage /> },
      { path: "signup", element: <RegisterPage /> },
    ],
    errorElement: <ErrorPage />,
  },
]);
