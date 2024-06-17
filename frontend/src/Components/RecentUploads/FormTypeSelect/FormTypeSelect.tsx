import React from "react";
import "./FormTypeSelect.css";

interface Props {
  onRecentPostsClick: () => void;
  onSavedPostsClick: () => void;
  activeView: "recent" | "saved";
}

const FormTypeSelect: React.FC<Props> = ({
  onRecentPostsClick,
  onSavedPostsClick,
  activeView,
}) => {
  return (
    <div className="form-type__select__container">
      <div
        className={`form-type__select-recent form-type__select ${activeView === "recent" ? "active" : ""}`}
        onClick={onRecentPostsClick}
      >
        All Uploads
      </div>
      <div
        className={`form-type__select-saved form-type__select ${activeView === "saved" ? "active" : ""}`}
        onClick={onSavedPostsClick}
      >
        Saved
      </div>
      <div
        className="form-type__select__underline"
        style={{
          transform:
            activeView === "recent"
              ? "translateX(0)"
              : "translateX(var(--form-select-width))",
        }}
      />
    </div>
  );
};

export default FormTypeSelect;
