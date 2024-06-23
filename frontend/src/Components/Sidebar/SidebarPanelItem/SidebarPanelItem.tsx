import { FC } from "react";
import { IconType } from "react-icons";
import "./SidebarPanelItem.css";

interface SidebarPanelItemProps {
  icon?: IconType;
  title: string;
}

const SidebarPanelItem: FC<SidebarPanelItemProps> = ({ icon: Icon, title }) => {
  return (
    <div className="sidebar__panel__item ">
      <div className="sidebar__panel__item__icon">
        {Icon && <Icon size={20} />}
      </div>
      <div className="sidebar__panel__item___title">{title}</div>
    </div>
  );
};

export default SidebarPanelItem;
