import { FC, useEffect, useState } from "react";
import "./Sidebar.css";
import SidebarPanelGroup from "./SidebarPanelGroup/SidebarPanelGroup.tsx";
import SidebarPanelItem from "./SidebarPanelItem/SidebarPanelItem.tsx";
import {
  GoFileMedia,
  GoHome,
  GoPerson,
  GoPlusCircle,
  GoStar,
} from "react-icons/go";
import { useAuth } from "../../Context/useAuth.tsx";

interface SidebarProps {}

const Sidebar: FC<SidebarProps> = () => {
  const { user } = useAuth();
  const [isMobileDisplay, setMobileDisplay] = useState<boolean>(false);

  useEffect(() => {
    const handleResize = () => {
      if (window.innerWidth < 850) {
        setMobileDisplay(true);
      } else {
        setMobileDisplay(false);
      }
    };

    handleResize();

    window.addEventListener("resize", handleResize);

    return () => {
      window.removeEventListener("resize", handleResize);
    };
  }, []);

  return isMobileDisplay ? (
    <div className="sidebar__mobile">
      <SidebarPanelItem icon={GoHome} linkTo={"/"} iconSize={25} />
      <SidebarPanelItem icon={GoFileMedia} linkTo={"/uploads"} iconSize={25} />
      <SidebarPanelItem
        icon={GoPlusCircle}
        linkTo={"/uploads/new"}
        iconSize={25}
      />
      <SidebarPanelItem
        icon={GoStar}
        linkTo={"/uploads/favourites"}
        iconSize={25}
      />
      <SidebarPanelItem icon={GoPerson} linkTo={"/account"} iconSize={25} />
    </div>
  ) : (
    <div className="sidebar">
      <div className="sidebar__title">Koral</div>
      <div className="sidebar__content">
        <SidebarPanelGroup>
          <SidebarPanelItem title="Home" icon={GoHome} linkTo={"/"} />
          <SidebarPanelItem
            title="All Uploads"
            icon={GoFileMedia}
            linkTo={"/uploads"}
          />
          <SidebarPanelItem
            title="Favourites"
            icon={GoStar}
            linkTo="/uploads/favourites"
          />
          <SidebarPanelItem
            title="New Upload"
            icon={GoPlusCircle}
            linkTo={"/uploads/new"}
          />
        </SidebarPanelGroup>
        <SidebarPanelGroup title="Collections">
          <SidebarPanelItem title="Saved" icon={GoHome} linkTo={"/saved"} />
          <SidebarPanelItem
            title="All Uploads"
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
