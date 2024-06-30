import { FC } from "react";
import { GoHeart, GoHeartFill } from "react-icons/go";
import "./resources/styles/FavouriteButton.css";

interface FavouriteButtonProps {
  isFavourited: boolean;
  setFavourite: () => void;
}

const FavouriteButton: FC<FavouriteButtonProps> = ({
  isFavourited,
  setFavourite,
}) => {
  return (
    <div>
      <button
        className={`upload__card-favourite-button ${isFavourited ? "favourited" : ""}`}
        onClick={setFavourite}
      >
        {isFavourited ? <GoHeartFill /> : <GoHeart />}
      </button>
    </div>
  );
};

export default FavouriteButton;
