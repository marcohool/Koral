import React, { useEffect } from "react";
import Navbar from "../../Components/Navbar/Navbar.tsx";
import "./HomePage.css";
import UploadTypeSelect from "../../Components/RecentUploads/UploadTypeSelect/UploadTypeSelect.tsx";
import { Upload, UploadType } from "../../Components/RecentUploads/types.ts";
import UploadGrid from "../../Components/UploadsDashboard/UploadGrid/UploadGrid.tsx";
import { uploadsGET } from "../../Services/UploadService.ts";
import Spinner from "../../Components/Spinner/Spinner.tsx";

interface Props {}

const HomePage: React.FC<Props> = () => {
  const [uploads, setUploads] = React.useState<Upload[]>();
  const [activeView, setActiveView] = React.useState<UploadType>(
    UploadType.All,
  );

  useEffect(() => {
    getUploads();
  }, []);

  const getUploads = () => {
    uploadsGET().then((res) => {
      res?.data && setUploads(res?.data);
    });
  };

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
          {uploads ? (
            <UploadGrid uploads={uploads} />
          ) : (
            <div className="home__spinner">
              <Spinner height={"4.5rem"} />
            </div>
          )}
        </div>
      </div>
    </div>
  );
};

export default HomePage;
