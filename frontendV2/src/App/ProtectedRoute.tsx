import { FC, ReactNode } from 'react';
import { Navigate, useLocation } from 'react-router-dom';
import useAuth from 'context/useAuth';

const ProtectedRoute: FC<{ children: ReactNode }> = ({ children }) => {
  const { token } = useAuth();
  const location = useLocation();

  if (!token) {
    return <Navigate to="/login" state={{ from: location }} replace />;
  }

  return children;
};

export default ProtectedRoute;
