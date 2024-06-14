import "./App.css";
import { Outlet } from "react-router-dom";
import { UserProvider } from "./Context/useAuth.tsx";
import { ToastContainer } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";
import { CustomSlide } from "./Utils/Toast/toastConfig.ts";

function App() {
  return (
    <div className="App">
      <UserProvider>
        <Outlet />
        <ToastContainer
          position="top-right"
          autoClose={4500}
          closeButton={true}
          hideProgressBar={true}
          newestOnTop={false}
          closeOnClick
          rtl={false}
          pauseOnFocusLoss={false}
          draggable
          theme="light"
          transition={CustomSlide}
          limit={4}
        />
      </UserProvider>
    </div>
  );
}

export default App;
