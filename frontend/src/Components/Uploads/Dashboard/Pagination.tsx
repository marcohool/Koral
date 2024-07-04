import { FC } from "react";
import Button from "../../Button/Button.tsx";
import { ButtonType } from "../../Button/types.ts";
import { GoChevronLeft, GoChevronRight } from "react-icons/go";
import "./resources/styles/Pagination.css";

interface PaginationProps {
  currentPage: number;
  totalPages: number;
  onNewPage: (pageNumber: number) => void;
}

const Pagination: FC<PaginationProps> = ({
  currentPage,
  totalPages,
  onNewPage,
}) => {
  return (
    <div className="pagination">
      {currentPage > 1 && (
        <Button
          type={ButtonType.tertiary}
          onClick={() => onNewPage(currentPage - 1)}
        >
          <GoChevronLeft size={30} />
        </Button>
      )}
      {currentPage} of {totalPages} pages
      {currentPage < totalPages && (
        <Button
          type={ButtonType.tertiary}
          onClick={() => onNewPage(currentPage + 1)}
        >
          {" "}
          <GoChevronRight size={30} />
        </Button>
      )}
    </div>
  );
};

export default Pagination;
