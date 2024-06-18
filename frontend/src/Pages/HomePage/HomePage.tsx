import React from "react";
import Navbar from "../../Components/Navbar/Navbar.tsx";
import "./HomePage.css";
import ViewTypeSelect from "../../Components/RecentUploads/ViewTypeSelect/ViewTypeSelect.tsx";
import { ViewType } from "../../Components/RecentUploads/types.ts";

interface Props {}

const HomePage: React.FC<Props> = () => {
  const [activeView, setActiveView] = React.useState<ViewType>(ViewType.All);

  const handleRecentPostsClick = () => setActiveView(ViewType.All);
  const handleSavedPostsClick = () => setActiveView(ViewType.Saved);

  return (
    <div className="home__layout">
      <Navbar isScrolled={true} />
      <div className="home__content">
        <div className="home__header">
          <div className="home__header-start">
            <h1 className="home__titles-title">Recent Uploads</h1>
          </div>
          <div className="home__header-end">
            <button className="home__header-upload-button">Upload</button>
          </div>
        </div>
        <div className="home__upload-type-select">
          <ViewTypeSelect
            activeView={activeView}
            onRecentPostsClick={handleRecentPostsClick}
            onSavedPostsClick={handleSavedPostsClick}
          />
        </div>
        <div className="home__recent-uploads"></div>
      </div>
    </div>
  );
};

export default HomePage;
