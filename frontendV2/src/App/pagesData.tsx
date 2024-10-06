import { routerType } from './router.types';
import LandingPage from 'pages/landing/LandingPage';
import AboutPage from 'pages/about/AboutPage';
import ComponentsPage from 'pages/components/ComponentsPage';

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
  },
];

export default pagesData;
