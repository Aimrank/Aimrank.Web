import { defineComponent, reactive, watch } from "vue";
import { useProfileUser } from "@/profile/hooks/useProfileUser";
import {
  useAcceptFriendshipInvitation,
  useDeclineFriendshipInvitation,
  useBlockUser,
  useUnblockUser,
  useDeleteFriendship,
  useFriendsView
} from "@/profile/graphql";
import BaseButton from "@/common/components/BaseButton";

interface IState {
  friends: any[];
  invites: any[];
  blocked: any[];
}

const Friends = defineComponent({
  components: {
    BaseButton
  },
  setup() {
    const state = reactive<IState>({
      blocked: [],
      friends: [],
      invites: []
    });

    const { profileUserId, isCurrentUserProfile } = useProfileUser();
    const { result, fetch } = useFriendsView(profileUserId);

    watch(
      () => result.value,
      () => {
        state.blocked = result.value?.blockedUsers?.nodes ?? [];
        state.friends = result.value?.user?.friends?.nodes ?? [];
        state.invites = result.value?.friendshipInvitations?.nodes ?? [];
      }
    );

    const { mutate: acceptInvitation } = useAcceptFriendshipInvitation();
    const { mutate: declineInvitation } = useDeclineFriendshipInvitation();
    const { mutate: blockUser } = useBlockUser();
    const { mutate: unblockUser } = useUnblockUser();
    const { mutate: deleteFriendship } = useDeleteFriendship();

    const onAccept = async (userId: string) => {
      await acceptInvitation({ userId });

      const user = state.invites.find(u => u.id === userId);

      if (user) {
        state.invites = state.invites.filter(u => u.id !== userId);
        state.friends = [...state.friends, user];
      }
    }

    const onDecline = async (userId: string) => {
      await declineInvitation({ userId });

      state.invites = state.invites.filter(u => u.id !== userId);
    }

    const onBlock = async (userId: string) => {
      await blockUser({ userId });

      const user = state.friends.find(u => u.id === userId);

      if (user) {
        state.friends = state.friends.filter(u => u.id !== userId);
        state.blocked = [...state.blocked, user];
      }
    }

    const onUnblock = async (userId: string) => {
      await unblockUser({ userId })
      await fetch();
    }

    const onDelete = async (userId: string) => {
      await deleteFriendship({ userId });

      state.friends = state.friends.filter(u => u.id !== userId);
    }

    return {
      state,
      isCurrentUserProfile,
      onAccept,
      onDecline,
      onBlock,
      onUnblock,
      onDelete
    };
  }
});

export default Friends;
