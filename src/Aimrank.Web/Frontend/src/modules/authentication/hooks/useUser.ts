import { reactive, readonly } from "vue";
import { IUser } from "../models/IUser";

interface IUserState {
  user: IUser | null;
}

const state = reactive<IUserState>({
  user: null
});

const setUser = (user: IUser | null) => {
  state.user = user;
}

export const useUser = () => ({
  state: readonly(state),
  setUser
});
