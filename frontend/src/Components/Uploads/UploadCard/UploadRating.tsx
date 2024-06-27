import { FC } from "react";
import { RiStarFill } from "react-icons/ri";
import "./resources/styles/UploadRating.css";

interface UploadRatingProps {
  rating?: number;
}

const UploadRating: FC<UploadRatingProps> = ({ rating }) => {
  const ratingScore = rating ?? 0;
  const ratingClass =
    ratingScore > 5 ? "high" : ratingScore > 2 ? "medium" : "low";

  return (
    <div className={`upload-rating ${ratingClass}`}>
      <RiStarFill /> {ratingScore}/10
    </div>
  );
};

export default UploadRating;
