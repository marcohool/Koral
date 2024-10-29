import { BrowserRouter } from 'react-router-dom';
import Router from './router';
import Toaster from 'components/sonner';
import { Navbar } from 'components/navbar';
import useAuth from '@/context/useAuth';

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
