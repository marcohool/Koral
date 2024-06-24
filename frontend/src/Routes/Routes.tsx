import { createBrowserRouter } from "react-router-dom";
import App from "../App.tsx";
import ErrorPage from "../Pages/ErrorPage/ErrorPage.tsx";
import RegisterPage from "../Pages/RegisterPage/RegisterPage.tsx";
import LoginPage from "../Pages/LoginPage/LoginPage.tsx";
import Root from "../Components/Root/Root.tsx";
import ProtectedRoute from "./ProtectedRoute.tsx";
import UploadsPage from "../Pages/UploadsPage/UploadsPage.tsx";
import NewHomePage from "../Pages/NewHomePage/HomePage.tsx";

export const router = createBrowserRouter([
  {
    path: "/",
    element: <App />,
    children: [
      {
        path: "",
        element: <Root />,
        children: [
          {
            path: "",
            element: (
              <ProtectedRoute>
                <NewHomePage />
              </ProtectedRoute>
            ),
          },
          {
            path: "uploads",
            element: (
              <ProtectedRoute>
                <UploadsPage />
              </ProtectedRoute>
            ),
          },
        ],
      },
      { path: "login", element: <LoginPage /> },
      { path: "signup", element: <RegisterPage /> },
    ],
    errorElement: <ErrorPage />,
  },
]);
