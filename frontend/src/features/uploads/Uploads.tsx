import { FC, useState } from "react";
import Dashboard from "../../Components/Dashboard/Dashboard.tsx";
import { UploadType } from "../../Pages/Protected/Uploads/GetUploads/types.ts";

interface UploadsGridProps {
  type: UploadType;
}

const Uploads: FC<UploadsGridProps> = () => {
  const [pageNumber, setPageNumber] = useState<number>();

  return (
    <div>
      <Dashboard title="All Uploads" />
    </div>
  );
};

export default Uploads;
