import useAuth from 'context/useAuth';
import { useMutation, useQueryClient } from '@tanstack/react-query';
import apiCall from 'utils/apiCall';
import { Upload } from 'shared/types/upload';
import { toast } from 'shared/hooks/use-toast';
import { AxiosError } from 'axios';

export const useFavourite = () => {
  const { token } = useAuth();
  const queryClient = useQueryClient();

  return useMutation<Upload, AxiosError, string, Upload>({
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
    onError: (_, id, context) => {
      const actionText = !context?.isFavourited
        ? 'favouriting your upload'
        : 'removing your upload from favourites';

      toast({
        variant: 'destructive',
        title: 'Something went wrong',
        description: `There was a problem ${actionText}`,
      });

      queryClient.setQueryData(['uploads', id], context);
    },
    onSettled: async () => {
      await queryClient.invalidateQueries({ queryKey: ['uploads'] });
    },
  });
};
