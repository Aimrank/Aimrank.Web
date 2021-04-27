import { reactive } from "vue";

interface IInitialAppStateUser {
  id: string;
  email: string;
  username: string;
  roles: string[];
}

interface IInitialAppState {
  error: string | null;
  user: IInitialAppStateUser | null;
}

const initialAppState = (window as any).initialAppState as IInitialAppState;

const state = reactive<IInitialAppState>({
  error: initialAppState.error,
  user: initialAppState.user
});

export const useInitialState = () => {
  const getError = () => {
    const error = state.error;
    state.error = null;
    return error;
  }

  const getUser = () => state.user;

  return { getError, getUser };
}
