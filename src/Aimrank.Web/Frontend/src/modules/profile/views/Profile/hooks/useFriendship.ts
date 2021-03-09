import { computed, ref } from "vue";
import { useRoute } from "vue-router";
import { friendshipService } from "~/services";
import { useUser } from "@/profile/hooks/useUser";
import { IFriendshipDto } from "@/profile/models/IFriendshipDto";
import { FriendshipState } from "@/profile/models/FriendshipState";

export const useFriendship = () => {
  const user = useUser();
  const route = useRoute();

  const userId = route.params.userId as string;

  const friendship = ref<IFriendshipDto | null>();

  const fetchFriendship = async () => {
    if (userId) {
      const result = await friendshipService.getFriendship(userId, user.state.user!.id);

      if (result.isOk()) {
        friendship.value = result.value;
      }
    }
  }

  const onInvite = async () => {
    if (userId) {
      const result = await friendshipService.inviteUser(userId);

      if (result.isOk()) {
        await fetchFriendship();
      }
    }
  }

  const onAccept = async () => {
    if (userId) {
      const result = await friendshipService.acceptInvitation(userId);

      if (result.isOk()) {
        await fetchFriendship();
      }
    }
  }

  const onDecline = async () => {
    if (userId) {
      const result = await friendshipService.declineInvitation(userId);

      if (result.isOk()) {
        await fetchFriendship();
      }
    }
  }

  const onDelete = async () => {
    if (userId) {
      const result = await friendshipService.deleteFriend(userId);

      if (result.isOk()) {
        await fetchFriendship();
      }
    }
  }

  const onUnblock = async () => {
    if (userId) {
      const result = await friendshipService.unblockUser(userId);

      if (result.isOk()) {
        await fetchFriendship();
      }
    }
  }

  const friendshipState = computed(() => {
    if (friendship.value && friendship.value.blockingUsersIds.length) return FriendshipState.Blocked;
    if (friendship.value && friendship.value.isAccepted) return FriendshipState.Active;
    if (friendship.value) return FriendshipState.Pending;
    return null;
  });

  return {
    friendship,
    friendshipState,
    fetchFriendship,
    onInvite,
    onAccept,
    onDecline,
    onDelete,
    onUnblock
  };
}
