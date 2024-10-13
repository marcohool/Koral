import { AxiosResponse, Method } from 'axios';
import { axiosInstance } from 'utils/axiosInstance';

const apiCall = async <TResponse, TRequest = undefined>(
  url: string,
  method: Method,
  token?: string,
  body?: TRequest,
): Promise<AxiosResponse<TResponse>> => {
  const config = {
    url,
    method,
    data: body as TRequest,
    headers: {
      Authorization: token ? `Bearer ${token}` : '',
    },
  };

  return await axiosInstance.request<TResponse>(config);
};

export default apiCall;
