import { useMutation, useQuery, useQueryClient } from '@tanstack/react-query';
import { AxiosError } from 'axios';
import apiCall from 'utils/apiCall';
import useAuth from 'context/useAuth';
import globalRouter from 'App/globalRouter';
import { Upload } from 'shared/types/upload';

export const useAddUpload = () => {
  const { token } = useAuth();

  return useMutation({
    mutationFn: async (file: File) => {
      const upload = new FormData();
      upload.append('Image', file);

      const response = await apiCall<Upload, FormData>(
        '/uploads',
        'POST',
        token ?? undefined,
        upload,
      );
      return response.data;
    },
    onSuccess: (response) => {
      console.log('Upload successful');
      globalRouter.navigate?.('/uploads/' + response.id);
    },
  });
};

export const useUpload = (id: string) => {
  const { token } = useAuth();
  const queryClient = useQueryClient();

  return useQuery<Upload, AxiosError>({
    queryKey: ['uploads', id],
    initialData: () =>
      queryClient
        .getQueryData<Upload[]>(['uploads'])
        ?.find((upload) => upload.id === id),
    initialDataUpdatedAt: () =>
      queryClient.getQueryState(['uploads'])?.dataUpdatedAt,
    queryFn: async () => {
      const response = await apiCall<Upload>(
        '/uploads/' + id,
        'GET',
        token ?? undefined,
      );
      return response.data;
    },
  });
};

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
