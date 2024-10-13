import { ReactElement } from 'react';

export enum Page {
  Landing,
  Components,
  About,
  Contact,
  Login,
  SignUp,
  Home,
}

export interface routerType {
  path: string;
  title: string;
  element: ReactElement;
  children?: routerType[];
  requireAuth?: boolean;
  page: Page;
}
