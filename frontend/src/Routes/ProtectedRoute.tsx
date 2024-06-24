import { FC, ReactNode } from "react";
import { Navigate, useLocation } from "react-router-dom";
import { useAuth } from "../Context/useAuth.tsx";

interface ProtectedRouteProps {
  children: ReactNode;
}

const ProtectedRoute: FC<ProtectedRouteProps> = ({ children }) => {
  const location = useLocation();
  const { isLoggedIn } = useAuth();

  return isLoggedIn() ? (
    <>{children}</>
  ) : (
    <Navigate to="/login" state={{ from: location }} replace />
  );
};

export default ProtectedRoute;
