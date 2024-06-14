import "./App.css";
import { Outlet } from "react-router-dom";
import { UserProvider } from "./Context/useAuth.tsx";
import Toast from "./Components/Toast/Toast.tsx";

function App() {
  return (
    <div className="App">
      <UserProvider>
        <Outlet />
        <Toast />
      </UserProvider>
    </div>
  );
}

export default App;
