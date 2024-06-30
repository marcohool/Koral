import { useAuth } from "../../Context/useAuth.tsx";
import LandingPage from "../../Pages/Public/LandingPage/LandingPage.tsx";
import Sidebar from "../Sidebar/Sidebar.tsx";
import { Outlet } from "react-router-dom";
import "./Root.css";

const Root = () => {
  const { isLoggedIn } = useAuth();

  return isLoggedIn() ? (
    <>
      <Sidebar />
      <div className="root__content">
        <Outlet />
      </div>
    </>
  ) : (
    <LandingPage />
  );
};

export default Root;
