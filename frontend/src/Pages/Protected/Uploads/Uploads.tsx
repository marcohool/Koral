import { FC } from "react";
import { Outlet } from "react-router-dom";

interface UploadsProps {}

const Uploads: FC<UploadsProps> = () => {
  return <Outlet />;
};

export default Uploads;
