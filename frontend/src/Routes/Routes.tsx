import { createBrowserRouter } from "react-router-dom";
import App from "../App.tsx";
import HomePage from "../Pages/HomePage/HomePage.tsx";
import ErrorPage from "../Pages/ErrorPage/ErrorPage.tsx";

export const router = createBrowserRouter([
  {
    path: "/",
    element: <App />,
    children: [{ path: "", element: <HomePage /> }],
    errorElement: <ErrorPage />,
  },
]);
