import React, { ReactElement } from 'react';
import useAuth from 'context/useAuth';
import { PageLayout } from 'App/router';

interface ConditionalRouteProps {
  component: ReactElement;
  fallback: ReactElement;
}

const ConditionalRoute: React.FC<ConditionalRouteProps> = ({
  component,
  fallback,
}) => {
  const { token } = useAuth();

  return token ? <PageLayout>{component}</PageLayout> : fallback;
};

export default ConditionalRoute;
