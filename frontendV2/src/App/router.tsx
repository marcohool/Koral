import pagesData from './pagesData';
import ProtectedRoute from './ProtectedRoute';
import { routerType } from './router.types';
import { Route, Routes, useNavigate } from 'react-router-dom';
import globalRouter from '@/App/globalRouter';

const Router = () => {
  globalRouter.navigate = useNavigate();

  const pageRoutes = pagesData.map(
    ({ path, title, element, requireAuth }: routerType) => {
      const routeElement = requireAuth ? (
        <ProtectedRoute>{element}</ProtectedRoute>
      ) : (
        element
      );

      return <Route key={title} path={`/${path}`} element={routeElement} />;
    },
  );

  return <Routes>{pageRoutes}</Routes>;
};

export default Router;
