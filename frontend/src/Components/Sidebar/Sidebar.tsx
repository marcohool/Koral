import { FC } from "react";
import "./Sidebar.css";
import SidebarPanelGroup from "./SidebarPanelGroup/SidebarPanelGroup.tsx";
import SidebarPanelItem from "./SidebarPanelItem/SidebarPanelItem.tsx";
import { GoFileMedia, GoHome, GoStar } from "react-icons/go";
import { useAuth } from "../../Context/useAuth.tsx";

interface SidebarProps {}

const Sidebar: FC<SidebarProps> = () => {
  const { user } = useAuth();

  return (
    <div className="sidebar">
      <div className="sidebar__title">Koral</div>
      <div className="sidebar__content">
        <SidebarPanelGroup>
          <SidebarPanelItem title="Home" icon={GoHome} linkTo={"/"} />
          <SidebarPanelItem
            title="AllUploads"
            icon={GoFileMedia}
            linkTo={"/uploads"}
          />
          <SidebarPanelItem
            title="Favourites"
            icon={GoStar}
            linkTo="/uploads/favourites"
          />
        </SidebarPanelGroup>
        <SidebarPanelGroup title="Collections">
          <SidebarPanelItem title="Saved" icon={GoHome} linkTo={"/saved"} />
          <SidebarPanelItem
            title="AllUploads"
            icon={GoFileMedia}
            linkTo={"/uploads"}
          />
          <SidebarPanelItem
            title="Collection"
            icon={GoFileMedia}
            linkTo={"/collection"}
          />
        </SidebarPanelGroup>
      </div>
      <footer className="sidebar__end">
        <div className="sidebar__end__user-email">{user}</div>
        <div className="sidebar__end__options"></div>
      </footer>
    </div>
  );
};

export default Sidebar;
