import axios from "axios";

const createSuccess = <T = undefined>(value?: T) => ({
  isOk: true,
  value,
  error: undefined
});

const createError = <E = undefined>(error?: E) => ({
  isOk: false,
  value: undefined,
  error
});

interface IResponseError {
  title: string;
}

export const signInWithSteam = async () => {
  try {
    const res = await axios.post("/api/steam/openid");

    window.location.href = res.data.location;

    return createSuccess();
  } catch (error) {
    return createError(error.response.data as IResponseError);
  }
}
