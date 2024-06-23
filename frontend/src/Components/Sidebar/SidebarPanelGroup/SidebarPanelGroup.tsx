import { FC, ReactNode } from "react";
import "./SidebarPanelGroup.css";

interface SidebarPanelProps {
  title?: string;
  children?: ReactNode;
}

const SidebarPanelGroup: FC<SidebarPanelProps> = ({ title, children }) => {
  return (
    <div className="sidebar__panel__group">
      <div className="sidebar__panel__group__title">{title}</div>
      <div className="sidebar__panel__group__content">{children}</div>
    </div>
  );
};

export default SidebarPanelGroup;
