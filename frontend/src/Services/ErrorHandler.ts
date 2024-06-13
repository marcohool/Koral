import axios from "axios";

export const handleError = (error: unknown) => {
  if (axios.isAxiosError(error)) {
    const err = error.response;

    if (Array.isArray(err?.data.errors)) {
      for (const val of err.data.errors) {
        console.log(val.description);
      }
    } else if (typeof err?.data.errors === "object") {
      for (const e in err!.data.errors) {
        console.log(err!.data.errors[e]);
      }
    } else if (err?.data) {
      console.log(err.data);
    } else if (err?.status === 401) {
      console.log("Unauthorized");
      window.history.pushState({}, "LoginPage", "/login");
    }
  } else {
    console.error("An unexpected error occurred", error);
  }
};
