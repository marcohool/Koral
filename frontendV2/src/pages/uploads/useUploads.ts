import { useQuery } from '@tanstack/react-query';
import { AxiosError } from 'axios';
import apiCall from 'utils/apiCall';
import useAuth from '@/context/useAuth';

export interface ClothingItem {
  id: string;
  name: string;
  description: string;
  brand: string;
  category: string;
  gender: string;
  imageUrl: string;
  sourceUrl: string;
  sourceRegion: string;
}

export interface Upload {
  id: string;
  title: string;
  status: string;
  imageUrl: string;
  isFavourited: boolean;
  createdOn: string;
  lastUpdatedOn: string;
  matchedClothingItems: ClothingItem[];
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
