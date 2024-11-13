import { BrowserRouter } from 'react-router-dom';
import Router from './router';
import { Navbar } from 'components/navbar';
import useAuth from 'context/useAuth';
import Toaster from 'components/toast/toaster';

function App() {
  const { token } = useAuth();

  return (
    <BrowserRouter>
      {token && <Navbar />}
      <Router />
      <Toaster />
    </BrowserRouter>
  );
}

export default App;
