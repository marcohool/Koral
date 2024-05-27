import "./App.css";
import { Outlet } from "react-router-dom";
import Navbar from "./Components/Navbar/Navbar.tsx";

function App() {
  return (
    <div className="App">
      <Navbar />
      <div className="content">
        <Outlet />
      </div>
    </div>
  );
}

export default App;
