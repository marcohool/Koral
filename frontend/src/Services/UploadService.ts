import { handleError, handleErrorV2 } from "./ErrorHandler.ts";
import axios from "axios";
import { Upload } from "../Components/Uploads/UploadsType.ts";
import { toast } from "react-toastify";

const API_URL = "https://localhost:5001";

const authenticatedAxios = (token: string) => {
  return axios.create({
    headers: {
      Authorization: `Bearer ${token}`, // Set the Authorization header
    },
  });
};

export const getUploadsAPI = async () => {
  try {
    return await authenticatedAxios(localStorage.getItem("token")!).get<
      Upload[]
    >(`${API_URL}/imageupload`);
  } catch (error) {
    handleError(error);
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
