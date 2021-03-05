import { reactive, readonly } from "vue";
import { userService } from "~/services";
import { IUserDto } from "@/profile/models/IUserDto";

interface IUserState {
  user: IUserDto | null;
}

const state = reactive<IUserState>({
  user: null
});

const setUser = (user: IUserDto | null) => {
  state.user = user;
}

const updateUser = async (userId: string) => {
  const result = await userService.getUserDetails(userId);

  if (result.isOk()) {
    setUser({
      id: result.value.id,
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
