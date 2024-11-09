import axios, { AxiosError, AxiosResponse } from 'axios';
import globalRouter from 'App/globalRouter';

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
      globalRouter.navigate?.('/login');

      return Promise.reject(new Error('Unauthorized. Please log in again.'));
    }

    return Promise.reject(error);
  },
);
