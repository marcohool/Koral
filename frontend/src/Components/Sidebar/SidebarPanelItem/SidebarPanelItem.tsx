import { FC, useEffect, useState } from "react";
import { IconType } from "react-icons";
import "./SidebarPanelItem.css";
import { Link } from "react-router-dom";
import { useLocation } from "react-router-dom";

interface SidebarPanelItemProps {
  icon?: IconType;
  title?: string;
  linkTo: string;
  iconSize?: number;
}

const SidebarPanelItem: FC<SidebarPanelItemProps> = ({
  icon: Icon,
  title,
  linkTo,
  iconSize = 20,
}) => {
  const [active, setActive] = useState<boolean>(false);
  const location = useLocation();

  useEffect(() => {
    if (location.pathname === linkTo) {
      setActive(true);
    } else {
      setActive(false);
    }
  }, [linkTo, location.pathname]);

  return (
    <Link to={linkTo}>
      <div className={`sidebar__panel__item ${active ? "active" : ""}`}>
        <div className="sidebar__panel__item__icon">
          {Icon && <Icon size={iconSize} />}
        </div>
        {title && <div className="sidebar__panel__item___title">{title}</div>}
      </div>
    </Link>
  );
};

export default SidebarPanelItem;
