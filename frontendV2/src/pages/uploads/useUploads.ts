import { useQuery } from '@tanstack/react-query';
import { AxiosError } from 'axios';
import apiCall from 'utils/apiCall';
import useAuth from '@/context/useAuth';

interface Upload {
  id: string;
  title: string;
  status: string;
  imageUrl: string;
  isFavorited: boolean;
  createdOn: string;
  lastUpdatedOn: string;
}

const useUploads = () => {
  const { token } = useAuth();

  return useQuery<Upload[], AxiosError>({
    queryKey: ['uploads'],
    queryFn: async () => {
      const response = await apiCall<Upload[]>(
        '/uploads',
        'GET',
        token ?? undefined,
      );
      return response.data;
    },
  });
};

export default useUploads;
