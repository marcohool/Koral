import useAuth from 'context/useAuth';
import { useMutation, useQueryClient } from '@tanstack/react-query';
import apiCall from 'utils/apiCall';
import { Upload } from 'shared/types/upload';

export const useFavourite = () => {
  const { token } = useAuth();
  const queryClient = useQueryClient();

  return useMutation<Upload, void, string, Upload>({
    mutationFn: async (id) => {
      return (
        await apiCall<Upload>(
          `/uploads/favourite/${id}`,
          'POST',
          token ?? undefined,
          undefined,
        )
      ).data;
    },
    onMutate: async (id) => {
      await queryClient.cancelQueries({ queryKey: ['uploads', id] });

      const upload = queryClient.getQueryData<Upload>(['uploads', id]);

      queryClient.setQueryData(['uploads', id], (old: Upload) => ({
        ...old,
        isFavourited: !upload?.isFavourited,
      }));

      return upload;
    },
    onError: (err, id, context) => {
      queryClient.setQueryData(['uploads', id], context);
    },
    onSettled: async () => {
      await queryClient.invalidateQueries({ queryKey: ['uploads'] });
    },
  });
};
