import { reactive } from "vue";

interface IInitialAppState {
  error: string;
}

const initialAppState = (window as any).initialAppState as IInitialAppState;

const state = reactive<IInitialAppState>({
  error: initialAppState.error
});

export const useInitialState = () => {
  const getError = () => {
    const error = state.error;
    state.error = "";
    return error;
  }

  return { getError };
}
