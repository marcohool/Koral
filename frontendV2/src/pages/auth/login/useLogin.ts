import { useMutation } from '@tanstack/react-query';
import { AxiosError } from 'axios';
import useAuth from 'context/useAuth';
import apiCall from 'utils/apiCall';

interface LoginData {
  email: string;
  password: string;
}

interface LoginResponse {
  token: string;
}

const useLogin = () => {
  const { setToken } = useAuth();

  return useMutation<{ token: string }, AxiosError, LoginData>({
    mutationFn: async (loginData) => {
      return apiCall<LoginResponse, LoginData>(
        '/users/login',
        'POST',
        undefined,
        loginData,
      ).then((response) => response.data);
    },
    onSuccess: (data) => {
      setToken(data.token);
      localStorage.setItem('authToken', data.token);
    },
    onError: (error) => {
      console.error('Login error:', error);
    },
  });
};

export default useLogin;
