import React, { useState } from "react";
import Navbar from "../../Components/Navbar/Navbar.tsx";
import "./HomePage.css";
import FormTypeSelect from "../../Components/RecentUploads/FormTypeSelect/FormTypeSelect.tsx";

interface Props {}

const HomePage: React.FC<Props> = () => {
  const [activeView, setActiveView] = React.useState<"recent" | "saved">(
    "recent",
  );

  const handleRecentPostsClick = () => setActiveView("recent");
  const handleSavedPostsClick = () => setActiveView("saved");

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
          <FormTypeSelect
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
