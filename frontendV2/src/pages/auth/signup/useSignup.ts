import useAuth from 'context/useAuth';
import { useMutation } from '@tanstack/react-query';
import { AxiosError } from 'axios';
import { axiosInstance } from 'utils/axiosInstance';

interface SignupData {
  email: string;
  password: string;
}

const useSignup = () => {
  const { setToken } = useAuth();

  return useMutation<{ token: string }, AxiosError, SignupData>({
    mutationFn: async (signupData) => {
      const response = await axiosInstance.post<{ token: string }>(
        '/users/signup',
        signupData,
      );
      return response.data;
    },
    onSuccess: (data) => {
      setToken(data.token);
      localStorage.setItem('authToken', data.token);
    },
    onError: (error) => {
      console.error('Signup error:', error);
    },
  });
};

export default useSignup;
