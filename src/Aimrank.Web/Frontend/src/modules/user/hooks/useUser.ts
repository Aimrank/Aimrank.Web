import { reactive, readonly } from "vue";
import { userService } from "~/services";
import { IUser } from "@/user/models/IUser";

interface IUserState {
  user: IUser | null;
}

const state = reactive<IUserState>({
  user: null
});

const setUser = (user: IUser | null) => {
  state.user = user;
}

const updateUser = async (userId: string) => {
  const result = await userService.getUserDetails(userId);

  if (result.isOk()) {
    setUser({
      id: result.value.userId,
      steamId: result.value.steamId,
      username: result.value.username
    });
  }
}

export const useUser = () => ({
  state: readonly(state),
  updateUser,
  setUser
});
