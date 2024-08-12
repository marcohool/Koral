import { createBrowserRouter } from "react-router-dom";
import App from "../App.tsx";
import ErrorPage from "../Pages/Public/Error/ErrorPage.tsx";
import Register from "../Pages/Public/Register/Register.tsx";
import Login from "../Pages/Public/Login/Login.tsx";
import Root from "../Components/Root/Root.tsx";
import ProtectedRoute from "./ProtectedRoute.tsx";
import Home from "../Pages/Protected/Home/Home.tsx";
import UploadsRoot from "../Pages/Protected/Uploads/UploadsRoot.tsx";
import NewUploadPage from "../Pages/Protected/Uploads/NewUpload/NewUploadPage.tsx";
import GetUploads from "../Pages/Protected/Uploads/GetUploads/GetUploads.tsx";
import { UploadType } from "../Pages/Protected/Uploads/GetUploads/types.ts";
import UploadPage from "../Pages/Protected/Uploads/Upload/UploadPage.tsx";
import UploadsGrid from "../features/uploads/UploadsGrid.tsx";

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
                <UploadsRoot />
              </ProtectedRoute>
            ),
            children: [
              {
                index: true,
                element: (
                  <ProtectedRoute>
                    <UploadsGrid />
                  </ProtectedRoute>
                ),
              },
              {
                path: "favourites",
                element: (
                  <ProtectedRoute>
                    <GetUploads key="favourites" type={UploadType.Favourites} />
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
              {
                path: ":id",
                element: (
                  <ProtectedRoute>
                    <UploadPage />
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
