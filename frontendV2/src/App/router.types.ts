import { ReactElement } from 'react';

export enum Page {
  Home,
  Components,
  About,
  Contact,
  Login,
  SignUp,
  Uploads,
  Upload,
}

export interface routerType {
  path: string;
  title: string;
  element: ReactElement;
  children?: routerType[];
  requireAuth?: boolean;
  page: Page;
}
