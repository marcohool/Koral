import axios, { AxiosError, AxiosResponse } from 'axios';
import useAuth from '@/context/useAuth';
import { useNavigate } from 'react-router-dom';

const apiBaseUrl = import.meta.env.VITE_KORAL_API_URL as string;

if (!apiBaseUrl) {
  throw new Error('KORAL_API_URL not defined');
}

export const axiosInstance = axios.create({
  baseURL: apiBaseUrl,
  headers: { 'Content-Type': 'application/json' },
});

axiosInstance.interceptors.response.use(
  (response: AxiosResponse) => response,
  (error: AxiosError) => {
    if (error.response?.status === 401) {
      const { setToken } = useAuth();
      const navigate = useNavigate();

      setToken(null);
      navigate('/login');

      return Promise.reject(new Error('Unauthorized. Please log in again.'));
    }

    return Promise.reject(error);
  },
);
