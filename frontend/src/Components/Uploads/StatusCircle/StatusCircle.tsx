import { FC } from "react";
import { StatusType } from "../UploadsType.ts";
import "./StatusCircle.css";

interface StatusCircleProps {
  status: StatusType;
}

const StatusCircle: FC<StatusCircleProps> = ({ status }) => {
  const getStatusClass = (statusType: StatusType) => {
    switch (statusType) {
      case 0:
        return "processing";
      case 1:
        return "processed";
      case 2:
        return "failed";
      default:
        return "";
    }
  };

  return <div className={`status-circle ${getStatusClass(status)}`}></div>;
};

export default StatusCircle;
