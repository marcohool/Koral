import { Page, routerType } from './router.types';
import LandingPage from 'pages/landing/LandingPage';
import AboutPage from 'pages/about/AboutPage';
import ComponentsPage from 'pages/components/ComponentsPage';
import LoginPage from 'pages/auth/login/LoginPage';
import SignupPage from 'pages/auth/signup/SignupPage';
import HomePage from 'pages/home';
import ConditionalRoute from '@/App/ConditionalRoute';

const pagesData: routerType[] = [
  {
    path: '',
    title: 'Home',
    element: (
      <ConditionalRoute component={<HomePage />} fallback={<LandingPage />} />
    ),
    page: Page.Home,
  },
  {
    path: 'about',
    title: 'About',
    element: <AboutPage />,
    page: Page.About,
  },
  {
    path: 'components',
    title: 'Components',
    element: <ComponentsPage />,
    page: Page.Components,
  },
  {
    path: 'contact',
    title: 'Contact',
    element: <LoginPage />,
    page: Page.Contact,
  },
  {
    path: 'login',
    title: 'Log in',
    element: <LoginPage />,
    page: Page.Login,
  },
  {
    path: 'signup',
    title: 'Sign Up',
    element: <SignupPage />,
    page: Page.SignUp,
  },
];

export const getPageData = (page: Page): routerType | undefined => {
  return pagesData.find((data) => data.page === page);
};

export default pagesData;
