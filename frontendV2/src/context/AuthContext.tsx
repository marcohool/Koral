import { createContext, FC, ReactNode, useEffect, useState } from 'react';

export interface AuthContextType {
  token: string | null;
  setToken: (token: string | null) => void;
}

const AuthContext = createContext<AuthContextType | undefined>(undefined);

export const AuthProvider: FC<{ children: ReactNode }> = ({ children }) => {
  const [token, setToken] = useState<string | null>(null);

  useEffect(() => {
    const storedToken = localStorage.getItem('authToken');
    setToken(storedToken);
  }, []);

  const updateToken = (newToken: string | null) => {
    if (newToken) {
      localStorage.setItem('authToken', newToken);
    } else {
      localStorage.removeItem('authToken');
    }
    setToken(newToken);
  };

  return (
    <AuthContext.Provider value={{ token, setToken: updateToken }}>
      {children}
    </AuthContext.Provider>
  );
};

export default AuthContext;
