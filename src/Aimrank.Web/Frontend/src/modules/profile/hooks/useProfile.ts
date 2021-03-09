import { reactive, readonly } from "vue";
import { friendshipService, userService } from "~/services";
import { IFriendshipDto } from "@/profile/models/IFriendshipDto";
import { IUserDto } from "@/profile/models/IUserDto";

interface IState {
  isLoading: boolean;
  user: IUserDto | null;
  friendship: IFriendshipDto | null;
}

const state = reactive<IState>({
  isLoading: false,
  user: null,
  friendship: null
});

const fetchPage = async (userId: string, currentUserId: string) => {
  state.isLoading = true;

  await fetchUser(userId);
  await fetchFriendship(userId, currentUserId);

  state.isLoading = false;
}

const fetchUser = async (userId: string) => {
  const result = await userService.getUserDetails(userId);

  if (result.isOk()) {
    state.user = result.value;
  }
}

const fetchFriendship = async (userId: string, currentUserId: string) => {
  const result = await friendshipService.getFriendship(userId, currentUserId);

  if (result.isOk()) {
    state.friendship = result.value;
  }
}

const inviteFriend = async (userId: string, currentUserId: string) => {
  const result = await friendshipService.inviteUser(userId);

  if (result.isOk()) {
    await fetchFriendship(userId, currentUserId);
  }
}

const acceptFriend = async (userId: string, currentUserId: string) => {
  const result = await friendshipService.acceptInvitation(userId);

  if (result.isOk()) {
    await fetchFriendship(userId, currentUserId);
  }
}

const declineFriend = async (userId: string, currentUserId: string) => {
  const result = await friendshipService.declineInvitation(userId);

  if (result.isOk()) {
    await fetchFriendship(userId, currentUserId);
  }
}

const deleteFriend = async (userId: string, currentUserId: string) => {
  const result = await friendshipService.deleteFriend(userId);

  if (result.isOk()) {
    await fetchFriendship(userId, currentUserId);
  }
}

const unblockFriend = async (userId: string, currentUserId: string) => {
  const result = await friendshipService.unblockUser(userId);

  if (result.isOk()) {
    await fetchFriendship(userId, currentUserId);
  }
}

export const useProfile = () => ({
  state: readonly(state),
  fetchPage,
  inviteFriend,
  acceptFriend,
  declineFriend,
  deleteFriend,
  unblockFriend
});
