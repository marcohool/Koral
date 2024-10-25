import React, { ReactElement } from 'react';
import useAuth from 'context/useAuth';

interface ConditionalRouteProps {
  component: ReactElement;
  fallback: ReactElement;
}

const ConditionalRoute: React.FC<ConditionalRouteProps> = ({
  component,
  fallback,
}) => {
  const { token } = useAuth();

  return token ? component : fallback;
};

export default ConditionalRoute;
