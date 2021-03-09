import { computed, defineComponent, onMounted, reactive } from "vue";
import { useRoute } from "vue-router";
import { friendshipService } from "~/services";
import { useUser } from "@/profile/hooks/useUser";
import { IUserDto } from "@/profile/models/IUserDto";
import BaseButton from "@/common/components/BaseButton";

interface IState {
  friends: IUserDto[];
  invites: IUserDto[];
  blocked: IUserDto[];
}

const Friends = defineComponent({
  components: {
    BaseButton
  },
  setup() {
    const user = useUser();
    const route = useRoute();

    const state = reactive<IState>({
      friends: [],
      invites: [],
      blocked: []
    });

    const userId = computed(() => route.params.userId || user.state.user?.id);

    const isSelf = computed(() => userId.value === user.state.user?.id);

    onMounted(async () => {
      if (userId.value) {
        const friends = await friendshipService.getFriendsList(userId.value as string);
        const invites = await friendshipService.getFriendshipInvitations();
        const blocked = await friendshipService.getBlockedUsers();

        if (friends.isOk() && invites.isOk() && blocked.isOk()) {
          state.friends = friends.value;
          state.invites = invites.value;
          state.blocked = blocked.value;
        }
      }
    });

    const onAccept = async (userId: string) => {
      const result = await friendshipService.acceptInvitation(userId);

      if (result.isOk()) {
        const user = state.invites.find(u => u.id === userId);

        if (user) {
          state.invites = state.invites.filter(u => u.id !== userId);
          state.friends = [...state.friends, user];
        }
      }
    }

    const onDecline = async (userId: string) => {
      const result = await friendshipService.declineInvitation(userId);

      if (result.isOk()) {
        state.invites = state.invites.filter(u => u.id !== userId);
      }
    }

    const onBlock = async (userId: string) => {
      const result = await friendshipService.blockUser(userId);

      if (result.isOk()) {
        const user = state.friends.find(u => u.id === userId);

        if (user) {
          state.friends = state.friends.filter(u => u.id !== userId);
          state.blocked = [...state.blocked, user];
        }
      }
    }

    const onUnblock = async (userId: string) => {
      const result = await friendshipService.unblockUser(userId);

      if (result.isOk()) {
        const friends = await friendshipService.getFriendsList(userId);

        if (friends.isOk()) {
          state.friends = friends.value;
          state.blocked = state.blocked.filter(u => u.id !== userId);
        }
      }
    }

    const onDelete = async (userId: string) => {
      const result = await friendshipService.deleteFriend(userId);

      if (result.isOk()) {
        state.friends = state.friends.filter(u => u.id !== userId);
      }
    }

    return {
      state,
      isSelf,
      onAccept,
      onDecline,
      onBlock,
      onUnblock,
      onDelete
    };
  }
});

export default Friends;
