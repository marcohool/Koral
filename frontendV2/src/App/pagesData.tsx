import { routerType } from "./router.types.ts";
import Landing from "../pages/Landing.tsx";
import About from "../pages/About.tsx";

const pagesData: routerType[] = [
  {
    path: "",
    title: "home",
    element: <Landing />,
  },
  {
    path: "about",
    title: "about",
    element: <About />,
  },
];

export default pagesData;
