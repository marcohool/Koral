import React from "react";
import "./ViewTypeSelect.css";
import { ViewType } from "../types.ts";

interface Props {
  onRecentPostsClick: () => void;
  onSavedPostsClick: () => void;
  activeView: ViewType;
}

const ViewTypeSelect: React.FC<Props> = ({
  onRecentPostsClick,
  onSavedPostsClick,
  activeView,
}) => {
  return (
    <div className="form-type__select__container">
      <div
        className={`form-type__select-recent form-type__select ${activeView === ViewType.All ? "active" : ""}`}
        onClick={onRecentPostsClick}
      >
        All Uploads
      </div>
      <div
        className={`form-type__select-saved form-type__select ${activeView === ViewType.Saved ? "active" : ""}`}
        onClick={onSavedPostsClick}
      >
        Saved
      </div>
      <div
        className="form-type__select__underline"
        style={{
          transform:
            activeView === ViewType.All
              ? "translateX(0)"
              : "translateX(var(--form-select-width))",
        }}
      />
    </div>
  );
};

export default ViewTypeSelect;
