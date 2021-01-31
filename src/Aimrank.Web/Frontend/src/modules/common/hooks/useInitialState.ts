import { reactive } from "vue";

interface IInitialAppState {
  error: string | null;
}

const initialAppState = (window as any).initialAppState as IInitialAppState;

const state = reactive<IInitialAppState>({
  error: initialAppState.error
});

export const useInitialState = () => {
  const getError = () => {
    const error = state.error;
    state.error = null;
    return error;
  }

  return { getError };
}
