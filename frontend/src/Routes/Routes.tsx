import { createBrowserRouter } from "react-router-dom";
import App from "../App.tsx";
import ErrorPage from "../Pages/ErrorPage/ErrorPage.tsx";
import RegisterPage from "../Pages/RegisterPage/RegisterPage.tsx";
import LoginPage from "../Pages/LoginPage/LoginPage.tsx";
import RootComponent from "../Components/RootComponent/RootComponent.tsx";
import ProtectedRoute from "./ProtectedRoute.tsx";
import UploadsPage from "../Pages/UploadsPage/UploadsPage.tsx";

export const router = createBrowserRouter([
  {
    path: "/",
    element: <App />,
    children: [
      { path: "", element: <RootComponent /> },
      { path: "login", element: <LoginPage /> },
      { path: "signup", element: <RegisterPage /> },
      {
        path: "uploads",
        element: (
          <ProtectedRoute>
            <UploadsPage />
          </ProtectedRoute>
        ),
      },
    ],
    errorElement: <ErrorPage />,
  },
]);
