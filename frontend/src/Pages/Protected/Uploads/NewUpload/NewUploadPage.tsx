import { FC } from "react";
import NewUploadDisplay from "../../../../Components/Uploads/NewUploadDisplay/NewUploadDisplay.tsx";

interface NewUploadPageProps {}

const NewUploadPage: FC<NewUploadPageProps> = () => {
  const handleClose = () => {};

  return (
    <div>
      <NewUploadDisplay onClose={handleClose} isModal={false} />
    </div>
  );
};

export default NewUploadPage;
