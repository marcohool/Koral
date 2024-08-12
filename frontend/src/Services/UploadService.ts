import { handleError, handleErrorV2 } from "./ErrorHandler.ts";
import axios from "axios";
import { Upload } from "../Components/Uploads/types.ts";
import { toast } from "react-toastify";

const API_URL = "https://localhost:5001";

const authenticatedAxios = (token: string) => {
  return axios.create({
    headers: {
      Authorization: `Bearer ${token}`,
    },
  });
};

export const getUploadsAPI = async (pageNumber: number) => {
  try {
    return await authenticatedAxios(localStorage.getItem("token")!).get<
      Upload[]
    >(`${API_URL}/uploads?pageNumber=${pageNumber}`);
  } catch (error) {
    handleError(error);
  }
};

export const getFavouriteUploadsAPI = async (pageNumber: number) => {
  try {
    return await authenticatedAxios(localStorage.getItem("token")!).get<
      Upload[]
    >(`${API_URL}/uploads/favourites?pageNumber=${pageNumber}`);
  } catch (error) {
    toast.error(handleErrorV2(error));
  }
};

export const favouriteUploadAPI = async (imageId: string) => {
  try {
    return await authenticatedAxios(localStorage.getItem("token")!).put(
      `${API_URL}/uploads/favourite/${imageId}`,
    );
  } catch (error) {
    toast.error(handleErrorV2(error));
  }
};

export const getUploadAPI = async (uploadId: number) => {
  try {
    return await authenticatedAxios(localStorage.getItem("token")!).get<Upload>(
      `${API_URL}/uploads/${uploadId}`,
    );
  } catch (error) {
    handleErrorV2(error);
  }
};

export const uploadImageAPI = async (formData: FormData) => {
  try {
    return await authenticatedAxios(localStorage.getItem("token")!).post(
      `${API_URL}/uploads`,
      formData,
    );
  } catch (error) {
    toast.error(handleErrorV2(error));
  }
};
