import { computed, defineComponent } from "vue";
import { FriendshipState } from "@/profile/models/FriendshipState";
import { useProfileUser } from "@/profile/hooks/useProfileUser";
import { useProfileStore } from "@/profile/hooks/useProfileStore";
import {
  useAcceptFriendshipInvitation,
  useDeclineFriendshipInvitation,
  useDeleteFriendship,
  useInviteUserToFriendsList,
  useUnblockUser
} from "@/profile/graphql";
import BaseButton from "@/common/components/BaseButton";

const FriendshipButtons = defineComponent({
  components: {
    BaseButton
  },
  setup() {
    const { profileUserId, currentUserId } = useProfileUser();
    const { state, setFriendship } = useProfileStore();

    const friendshipState = computed(() => {
      if (state.value?.friendship && state.value?.friendship?.blockingUsersIds?.length) return FriendshipState.Blocked;
      if (state.value?.friendship && state.value?.friendship?.isAccepted) return FriendshipState.Active;
      if (state.value?.friendship) return FriendshipState.Pending;
      return null;
    });

    const invitingUserId = computed(() => state.value?.friendship?.invitingUserId);
    const blockingUsersIds = computed(() => state.value?.friendship?.blockingUsersIds ?? []);

    const { mutate: inviteUser } = useInviteUserToFriendsList();
    const { mutate: acceptInvitation } = useAcceptFriendshipInvitation();
    const { mutate: declineInvitation } = useDeclineFriendshipInvitation();
    const { mutate: deleteFriendship } = useDeleteFriendship();
    const { mutate: unblockUser } = useUnblockUser();

    const onInvite = async (userId: string) => {
      const result = await inviteUser({ userId });
      setFriendship(result?.inviteUserToFriendsList?.query?.friendship);
    }

    const onAccept = async (userId: string) => {
      const result = await acceptInvitation({ userId });
      setFriendship(result?.acceptFriendshipInvitation?.query?.friendship);
    }

    const onDecline = async (userId: string) => {
      const result = await declineInvitation({ userId });
      setFriendship(result?.declineFriendshipInvitation?.query?.friendship);
    }
    
    const onDelete = async (userId: string) => {
      await deleteFriendship({ userId });
      setFriendship(undefined);
    }

    const onUnblock = async (userId: string) => {
      const result = await unblockUser({ userId });
      setFriendship(result?.unblockUser?.query?.friendship);
    }

    return {
      profileUserId,
      currentUserId,
      friendshipState,
      invitingUserId,
      blockingUsersIds,
      onInvite,
      onAccept,
      onDecline,
      onDelete,
      onUnblock,
      FriendshipState: Object.freeze(FriendshipState)
    };
  }
});

export default FriendshipButtons;
