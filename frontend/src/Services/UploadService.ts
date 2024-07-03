import { handleError, handleErrorV2 } from "./ErrorHandler.ts";
import axios from "axios";
import { Upload } from "../Components/Uploads/types.ts";
import { toast } from "react-toastify";

const API_URL = "https://localhost:5001";

const authenticatedAxios = (token: string) => {
  return axios.create({
    headers: {
      Authorization: `Bearer ${token}`, // Set the Authorization header
    },
  });
};

export const getUploadsAPI = async (pageNumber: number) => {
  try {
    return await authenticatedAxios(localStorage.getItem("token")!).get<
      Upload[]
    >(`${API_URL}/imageupload?pageNumber=${pageNumber}`);
  } catch (error) {
    handleError(error);
  }
};

export const getUploadCountAPI = async () => {
  try {
    return await authenticatedAxios(localStorage.getItem("token")!).get<number>(
      `${API_URL}/imageupload/totalcount`,
    );
  } catch (error) {
    toast.error(handleErrorV2(error));
  }
};

export const getFavouriteUploadsAPI = async () => {
  try {
    return await authenticatedAxios(localStorage.getItem("token")!).get<
      Upload[]
    >(`${API_URL}/imageupload/favourites`);
  } catch (error) {
    toast.error(handleErrorV2(error));
  }
};

export const favouriteUploadAPI = async (imageId: string) => {
  try {
    return await authenticatedAxios(localStorage.getItem("token")!).put(
      `${API_URL}/imageupload/favourite/${imageId}`,
    );
  } catch (error) {
    toast.error(handleErrorV2(error));
  }
};

export const uploadImageAPI = async (formData: FormData) => {
  try {
    return await authenticatedAxios(localStorage.getItem("token")!).post(
      `${API_URL}/imageupload`,
      formData,
    );
  } catch (error) {
    toast.error(handleErrorV2(error));
  }
};
