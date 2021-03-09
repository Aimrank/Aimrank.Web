import { computed, defineComponent } from "vue";
import { useRoute } from "vue-router";
import { useUser } from "@/profile/hooks/useUser";
import { useProfile } from "@/profile/hooks/useProfile";
import { FriendshipState } from "@/profile/models/FriendshipState";
import BaseButton from "@/common/components/BaseButton";

const FriendshipButtons = defineComponent({
  components: {
    BaseButton
  },
  setup() {
    const user = useUser();
    const route = useRoute();

    const userId = computed(() => route.params.userId as string || user.state.user!.id);
    const currentUserId = computed(() => user.state.user!.id);

    const {
      state,
      inviteFriend,
      acceptFriend,
      declineFriend,
      unblockFriend,
      deleteFriend
    } = useProfile();

    const friendshipState = computed(() => {
      if (state.friendship && state.friendship.blockingUsersIds.length) return FriendshipState.Blocked;
      if (state.friendship && state.friendship.isAccepted) return FriendshipState.Active;
      if (state.friendship) return FriendshipState.Pending;
      return null;
    });

    return {
      userId,
      currentUserId,
      state,
      friendshipState,
      inviteFriend,
      acceptFriend,
      declineFriend,
      unblockFriend,
      deleteFriend,
      FriendshipState: Object.freeze(FriendshipState)
    };
  }
});

export default FriendshipButtons;
