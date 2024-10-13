import { useMutation } from '@tanstack/react-query';
import { axiosInstance } from 'utils/axiosInstance';
import { AxiosError } from 'axios';
import useAuth from 'context/useAuth';

interface LoginData {
  email: string;
  password: string;
}

const useLogin = () => {
  const { setToken } = useAuth();

  return useMutation<{ token: string }, AxiosError, LoginData>({
    mutationFn: async (loginData) => {
      const response = await axiosInstance.post<{ token: string }>(
        '/users/login',
        loginData,
      );
      return response.data;
    },
    onSuccess: (data) => {
      setToken(data.token);
      localStorage.setItem('authToken', data.token);
    },
    onError: (error) => {
      console.error('Login failed:', error.response?.data);
    },
  });
};

export default useLogin;
