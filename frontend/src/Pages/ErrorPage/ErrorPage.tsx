import React from "react";
import Navbar from "../../Components/Navbar/Navbar.tsx";
import ErrorDisplay from "../../Components/ErrorDisplay/ErrorDisplay.tsx";
import "./ErrorPage.css";

interface Props {}

const ErrorPage: React.FC<Props> = () => {
  return (
    <div>
      <Navbar isScrolled={true} />
      <div className="error__div">
        <ErrorDisplay />
      </div>
    </div>
  );
};

export default ErrorPage;
