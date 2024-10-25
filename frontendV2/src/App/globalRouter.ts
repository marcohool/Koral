import { NavigateFunction } from 'react-router-dom';

const globalRouter = { navigate: null } as {
  navigate: NavigateFunction | null;
};

export default globalRouter;
