import React from "react";
import "./UploadTypeSelect.css";
import { UploadType } from "../types.ts";

interface Props {
  onRecentPostsClick: () => void;
  onSavedPostsClick: () => void;
  activeView: UploadType;
}

const UploadTypeSelect: React.FC<Props> = ({
  onRecentPostsClick,
  onSavedPostsClick,
  activeView,
}) => {
  return (
    <div className="form-type__select__container">
      <div
        className={`form-type__select-recent form-type__select ${activeView === UploadType.All ? "active" : ""}`}
        onClick={onRecentPostsClick}
      >
        All Uploads
      </div>
      <div
        className={`form-type__select-saved form-type__select ${activeView === UploadType.Saved ? "active" : ""}`}
        onClick={onSavedPostsClick}
      >
        Saved
      </div>
      <div
        className="form-type__select__underline"
        style={{
          transform:
            activeView === UploadType.All
              ? "translateX(0)"
              : "translateX(var(--form-select-width))",
        }}
      />
    </div>
  );
};

export default UploadTypeSelect;
