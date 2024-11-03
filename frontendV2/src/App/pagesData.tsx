import { Page, routerType } from './router.types';
import LandingPage from 'pages/landing/LandingPage';
import AboutPage from 'pages/about/AboutPage';
import ComponentsPage from 'pages/components/ComponentsPage';
import LoginPage from 'pages/auth/login/LoginPage';
import SignupPage from 'pages/auth/signup/SignupPage';
import HomePage from 'pages/home';
import ConditionalRoute from '@/App/ConditionalRoute';
import UploadsPage from 'pages/uploads/UploadsPage';
import UploadPage from 'pages/uploads/upload';
import { Outlet } from 'react-router-dom';

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
  {
    path: 'uploads',
    title: 'Uploads',
    element: <Outlet />,
    page: Page.Uploads,
    requireAuth: true,
    children: [
      {
        path: '',
        title: 'Uploads',
        element: <UploadsPage />,
        page: Page.Uploads,
      },
      {
        path: ':id',
        title: 'Upload',
        element: <UploadPage />,
        page: Page.Upload,
      },
    ],
  },
];

export const getPageData = (page: Page): routerType | undefined => {
  return pagesData.find((data) => data.page === page);
};

export default pagesData;
