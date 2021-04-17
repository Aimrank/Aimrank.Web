import { reactive } from "vue";
import { ErrorResponse, IFieldErrors } from "@/common/hooks/ErrorResponse";

interface IResponseErrorsState {
  errorMessage: string;
  errors: IFieldErrors;
}

export const useResponseErrors = () => {
  const state = reactive<IResponseErrorsState>({
    errorMessage: "",
    errors: {}
  });

  const setErrors = (response: ErrorResponse) => {
    state.errorMessage = response.title ?? "";
    state.errors = response.errors ?? {};
  }

  const clearErrors = () => {
    state.errorMessage = "";
    state.errors = {};
  }

  return {
    state,
    setErrors,
    clearErrors
  };
}
