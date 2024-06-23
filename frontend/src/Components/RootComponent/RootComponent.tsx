import { useAuth } from "../../Context/useAuth.tsx";
import LandingPage from "../../Pages/LandingPage/LandingPage.tsx";
import NewHomePage from "../../Pages/NewHomePage/HomePage.tsx";

const RootComponent = () => {
  const { isLoggedIn } = useAuth();

  // return isLoggedIn() ? <HomePage /> : <LandingPage />;
  return isLoggedIn() ? <NewHomePage /> : <LandingPage />;
};

export default RootComponent;
