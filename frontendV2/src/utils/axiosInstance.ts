import axios from 'axios';

const apiBaseUrl = import.meta.env.VITE_KORAL_API_URL as string;

if (!apiBaseUrl) {
  throw new Error('KORAL_API_URL not defined');
}

export const axiosInstance = axios.create({
  baseURL: apiBaseUrl,
  headers: { 'Content-Type': 'application/json' },
});
