import { routerType } from "./router.types";
import Landing from "pages/Landing";
import About from "pages/About";
import Components from "pages/Components";

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
  {
    path: "components",
    title: "components",
    element: <Components />,
  }
];

export default pagesData;
