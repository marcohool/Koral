import { FC } from "react";
import { useParams } from "react-router";

interface UploadPageProps {}

const UploadPage: FC<UploadPageProps> = () => {
  const { id } = useParams();

  return (
    <div>
      <h1>New Upload Page</h1>
      <p>Upload ID: {id}</p>
    </div>
  );
};

export default UploadPage;
