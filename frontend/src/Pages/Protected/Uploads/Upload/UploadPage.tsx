import { FC, useEffect, useState } from "react";
import { useParams } from "react-router";
import "./UploadPage.css";
import { Upload } from "../../../../Components/Uploads/types.ts";

interface UploadPageProps {}

const UploadPage: FC<UploadPageProps> = () => {
  const { id } = useParams();
  const [upload, setUpload] = useState<Upload>();

  useEffect(() => {});

  return (
    <section className="default-content">
      <div className="upload__hero">
        <div className="upload__photo"></div>
        <div className="upload__content"></div>
      </div>
    </section>
  );
};

export default UploadPage;
