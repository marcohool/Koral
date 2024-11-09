import pagesData from './pagesData';
import ProtectedRoute from './ProtectedRoute';
import { routerType } from './router.types';
import { Route, Routes, useNavigate } from 'react-router-dom';
import globalRouter from 'App/globalRouter';

const Router = () => {
  globalRouter.navigate = useNavigate();

  const pageRoutes = pagesData.map(
    ({ path, title, element, requireAuth, children }: routerType) => {
      const routeElement = requireAuth ? (
        <ProtectedRoute>{element}</ProtectedRoute>
      ) : (
        element
      );

      const childRoutes = children?.map(
        ({ path: childPath, title: childTitle, element: childElement }) => (
          <Route
            key={childTitle}
            path={childPath}
            element={
              requireAuth ? (
                <ProtectedRoute>{childElement}</ProtectedRoute>
              ) : (
                childElement
              )
            }
          />
        ),
      );

      return (
        <Route
          key={title}
          path={`/${path}`}
          element={routeElement}
          children={childRoutes}
        />
      );
    },
  );

  return <Routes>{pageRoutes}</Routes>;
};

export default Router;
