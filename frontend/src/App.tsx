import "./App.css";
import { Outlet } from "react-router-dom";
import { UserProvider } from "./Context/useAuth.tsx";

function App() {
  return (
    <div className="App">
      <UserProvider>
        <Outlet />
      </UserProvider>
    </div>
  );
}

export default App;
