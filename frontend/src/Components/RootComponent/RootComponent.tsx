import { useAuth } from "../../Context/useAuth.tsx";
import LandingPage from "../../Pages/LandingPage/LandingPage.tsx";
import HomePage from "../../Pages/HomePage/HomePage.tsx";

const RootComponent = () => {
  const { isLoggedIn } = useAuth();

  return isLoggedIn() ? <HomePage /> : <LandingPage />;
};

export default RootComponent;
