import { FC } from "react";
import NewUploadDisplay from "../../../../Components/Uploads/NewUploadDisplay/NewUploadDisplay.tsx";
import "./NewUploadPage.css";

interface NewUploadPageProps {}

const NewUploadPage: FC<NewUploadPageProps> = () => {
  const handleClose = () => {};

  return (
    <div className="upload__wrapper">
      <NewUploadDisplay
        onClose={handleClose}
        isModal={false}
        className={"upload__form"}
      />
    </div>
  );
};

export default NewUploadPage;
