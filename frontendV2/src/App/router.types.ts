import { ReactElement } from "react";

export interface routerType {
  path: string;
  title: string;
  element: ReactElement;
  children?: routerType[];
}
