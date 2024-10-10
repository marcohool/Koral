import { ReactElement } from 'react';

export enum Page {
  Home,
  Components,
  About,
  Contact,
  Login,
  SignUp,
}

export interface routerType {
  path: string;
  title: string;
  element: ReactElement;
  children?: routerType[];
  page: Page;
}
