import pagesData from './pagesData';
import ProtectedRoute from './ProtectedRoute';
import { routerType } from './router.types';
import { Route, Routes, useNavigate } from 'react-router-dom';
import globalRouter from '@/App/globalRouter';
import { Navbar } from 'components/navbar';
import { FC, ReactNode } from 'react';

export const PageConfiguration: FC<{ children: ReactNode }> = ({
  children,
}) => {
  return (
    <>
      <Navbar />
      <section className="flex max-w-content mx-auto">{children}</section>
    </>
  );
};

const Router = () => {
  globalRouter.navigate = useNavigate();

  const pageRoutes = pagesData.map(
    ({ path, title, element, requireAuth }: routerType) => {
      const routeElement = requireAuth ? (
        <ProtectedRoute>
          <PageConfiguration>{element}</PageConfiguration>
        </ProtectedRoute>
      ) : (
        element
      );

      return <Route key={title} path={`/${path}`} element={routeElement} />;
    },
  );

  return <Routes>{pageRoutes}</Routes>;
};

export default Router;
