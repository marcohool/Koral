import { routerType } from './router.types';
import LandingPage from '@pages/landing/LandingPage.tsx';
import AboutPage from '@pages/about/AboutPage.tsx';
import ComponentsPage from '@pages/components/ComponentsPage.tsx';

const pagesData: routerType[] = [
  {
    path: '',
    title: 'home',
    element: <LandingPage />,
  },
  {
    path: 'about',
    title: 'about',
    element: <AboutPage />,
  },
  {
    path: 'components',
    title: 'components',
    element: <ComponentsPage />,
  }
];

export default pagesData;
