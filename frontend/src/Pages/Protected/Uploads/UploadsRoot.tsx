import { FC } from "react";
import { Outlet } from "react-router-dom";

interface UploadsProps {}

const UploadsRoot: FC<UploadsProps> = () => {
  return <Outlet />;
};

export default UploadsRoot;
