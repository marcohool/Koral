import { createBrowserRouter } from "react-router-dom";
import App from "../App.tsx";
import ErrorPage from "../Pages/Public/Error/ErrorPage.tsx";
import Register from "../Pages/Public/Register/Register.tsx";
import Login from "../Pages/Public/Login/Login.tsx";
import Root from "../Components/Root/Root.tsx";
import ProtectedRoute from "./ProtectedRoute.tsx";
import AllUploads from "../Pages/Protected/AllUploads/AllUploads.tsx";
import Home from "../Pages/Protected/Home/Home.tsx";

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
                <Home />
              </ProtectedRoute>
            ),
          },
          {
            path: "uploads",
            element: (
              <ProtectedRoute>
                <AllUploads />
              </ProtectedRoute>
            ),
          },
        ],
      },
      { path: "login", element: <Login /> },
      { path: "signup", element: <Register /> },
    ],
    errorElement: <ErrorPage />,
  },
]);
