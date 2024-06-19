import React from "react";
import Navbar from "../../Components/Navbar/Navbar.tsx";
import "./HomePage.css";
import UploadTypeSelect from "../../Components/RecentUploads/UploadTypeSelect/UploadTypeSelect.tsx";
import { Upload, UploadType } from "../../Components/RecentUploads/types.ts";
import UploadGrid from "../../Components/RecentUploads/UploadGrid/UploadGrid.tsx";

interface Props {}

const HomePage: React.FC<Props> = () => {
  const [activeView, setActiveView] = React.useState<UploadType>(
    UploadType.All,
  );

  const uploads: Upload[] = [
    {
      id: "1",
      title: "Upload 1",
      description: "Description 1",
      saved: false,
      date: "2021-06-01",
    },
    {
      id: "2",
      title: "Upload 2",
      description: "Description 2",
      saved: true,
      date: "2021-06-02",
    },
    {
      id: "3",
      title: "Upload 3",
      description: "Description 3",
      saved: false,
      date: "2021-06-03",
    },
    {
      id: "4",
      title: "Upload 4",
      description: "Description 4",
      saved: true,
      date: "2021-06-04",
    },
    {
      id: "5",
      title: "Upload 5",
      description: "Description 5",
      saved: false,
      date: "2021-06-05",
    },
  ];

  const handleRecentPostsClick = () => setActiveView(UploadType.All);
  const handleSavedPostsClick = () => setActiveView(UploadType.Saved);

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
          <UploadTypeSelect
            activeView={activeView}
            onRecentPostsClick={handleRecentPostsClick}
            onSavedPostsClick={handleSavedPostsClick}
          />
        </div>
        <div className="home__recent-uploads">
          <UploadGrid uploads={uploads} />
        </div>
      </div>
    </div>
  );
};

export default HomePage;
