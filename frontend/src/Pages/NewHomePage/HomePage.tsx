import { FC } from "react";
import Sidebar from "../../Components/Sidebar/Sidebar.tsx";
import "./HomePage.css";

interface NewHomePageProps {}

const NewHomePage: FC<NewHomePageProps> = () => {
  return (
    <div>
      <Sidebar />
      <div className="new-home-page">NewHomePage</div>
    </div>
  );
};

export default NewHomePage;
