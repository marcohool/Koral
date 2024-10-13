import { FC } from 'react';
import useUploads from 'pages/uploads/useUploads';

const HomePage: FC = () => {
  const { data, isLoading } = useUploads();

  if (isLoading) {
    return <p>Loading...</p>;
  }

  return (
    <div>
      {data && data.length > 0 ? (
        data.map((upload, index) => (
          <p key={index}>
            Upload {index + 1}, {upload.title}
          </p>
        ))
      ) : (
        <p>None</p>
      )}
    </div>
  );
};

export default HomePage;
