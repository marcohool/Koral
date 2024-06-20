import { handleError } from "./ErrorHandler.ts";
import axios from "axios";
import { Upload } from "../Components/RecentUploads/types.ts";

const API_URL = "https://localhost:5001";

const authenticatedAxios = (token: string) => {
  return axios.create({
    headers: {
      Authorization: `Bearer ${token}`, // Set the Authorization header
    },
  });
};

export const uploadsGET = async () => {
  try {
    return await authenticatedAxios(localStorage.getItem("token")!).get<
      Upload[]
    >(`${API_URL}/imageupload`);
  } catch (error) {
    handleError(error);
  }
};
