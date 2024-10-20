import { BrowserRouter } from 'react-router-dom';
import Router from './router';
import Toaster from 'components/sonner';

function App() {
  return (
    <BrowserRouter>
      <Router />
      <Toaster />
    </BrowserRouter>
  );
}

export default App;
