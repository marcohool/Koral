import "./App.css";
import { Outlet } from "react-router-dom";
import { UserProvider } from "./Context/useAuth.tsx";
import { ToastContainer } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";

function App() {
  return (
    <div className="App">
      <UserProvider>
        <Outlet />
        <ToastContainer />
      </UserProvider>
    </div>
  );
}

export default App;
