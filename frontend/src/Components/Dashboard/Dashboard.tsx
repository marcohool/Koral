import { FC } from "react";
import Button from "../Button/Button.tsx";
import { ButtonType } from "../Button/types.ts";
import { GoPlus } from "react-icons/go";
import Card from "../Uploads/Card/Card.tsx";
import Pagination from "../Uploads/Dashboard/Pagination.tsx";
import Spinner from "../Spinner/Spinner.tsx";
import "./Dashboard.css";
import { Upload } from "../Uploads/types.ts";

interface DashboardProps<T> {
  title: string;
  entities: T[];
  pageNumber: number;
  totalPages: number;
  onNewPage: (pageNumber: number) => void;
  onAddClick?: () => void;
}

const Dashboard: FC<DashboardProps<Upload>> = ({
  title,
  entities,
  pageNumber,
  totalPages,
  onAddClick,
}) => {
  const onNewPage = (pageNumber: number) => {
    onNewPage(pageNumber);
  };

  return (
    <div className="grid">
      <div className="uploads-dashboard__titles">
        <h3 className="uploads-dashboard__titles-title">{title}</h3>
        <h3 className="uploads-dashboard__titles-upload-button">
          {onAddClick && (
            <Button
              value="New Upload"
              onClick={onAddClick}
              type={ButtonType.tertiary}
              styleOverride={{
                gap: "10px",
              }}
            >
              <GoPlus />
            </Button>
          )}
        </h3>
      </div>
      {entities ? (
        <>
          <div className="uploads-dashboard__grid">
            {entities.map((upload) => (
              <Card key={upload.imageId} upload={upload} />
            ))}
          </div>
          <Pagination
            currentPage={pageNumber}
            totalPages={totalPages}
            onNewPage={onNewPage}
          />
        </>
      ) : (
        <div className="uploads-dashboard__spinner">
          <Spinner height="60px" />
        </div>
      )}
    </div>
  );
};

export default Dashboard;
