import "./App.css";
import { Outlet } from "react-router-dom";
import { UserProvider } from "./Context/useAuth.tsx";
import { ToastContainer } from "react-toastify";

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
