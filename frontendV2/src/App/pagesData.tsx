import { routerType } from "./router.types";
import Landing from "pages/Landing";
import About from "pages/About";

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
