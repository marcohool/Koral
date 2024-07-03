import { createBrowserRouter } from "react-router-dom";
import App from "../App.tsx";
import ErrorPage from "../Pages/Public/Error/ErrorPage.tsx";
import Register from "../Pages/Public/Register/Register.tsx";
import Login from "../Pages/Public/Login/Login.tsx";
import Root from "../Components/Root/Root.tsx";
import ProtectedRoute from "./ProtectedRoute.tsx";
import AllUploads from "../Pages/Protected/Uploads/All/AllUploads.tsx";
import Home from "../Pages/Protected/Home/Home.tsx";
import FavouriteUploads from "../Pages/Protected/Uploads/Favourite/FavouriteUploads.tsx";
import Uploads from "../Pages/Protected/Uploads/Uploads.tsx";
import NewUploadPage from "../Pages/Protected/Uploads/NewUpload/NewUploadPage.tsx";

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
                <Uploads />
              </ProtectedRoute>
            ),
            children: [
              {
                index: true,
                element: (
                  <ProtectedRoute>
                    <AllUploads />
                  </ProtectedRoute>
                ),
              },
              {
                path: "favourites",
                element: (
                  <ProtectedRoute>
                    <FavouriteUploads />
                  </ProtectedRoute>
                ),
              },
              {
                path: "new",
                element: (
                  <ProtectedRoute>
                    <NewUploadPage />
                  </ProtectedRoute>
                ),
              },
            ],
          },
        ],
      },
      { path: "login", element: <Login /> },
      { path: "signup", element: <Register /> },
    ],
    errorElement: <ErrorPage />,
  },
]);
