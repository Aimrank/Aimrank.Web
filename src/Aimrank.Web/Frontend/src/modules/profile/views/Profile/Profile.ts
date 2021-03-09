import { computed, defineComponent, onMounted, reactive } from "vue";
import { useRoute } from "vue-router";
import { userService } from "~/services";
import { useFriendship } from "./hooks/useFriendship";
import { useUser } from "@/profile/hooks/useUser";
import { IUserDto } from "@/profile/models/IUserDto";
import { IFriendshipDto } from "@/profile/models/IFriendshipDto";
import { FriendshipState } from "@/profile/models/FriendshipState";
import BaseButton from "@/common/components/BaseButton";

interface IState {
  user: IUserDto | null;
  friendship: IFriendshipDto | null;
}

const Profile = defineComponent({
  components: {
    BaseButton
  },
  setup() {
    const state = reactive<IState>({
      user: null,
      friendship: null
    });

    const user = useUser();
    const route = useRoute();

    const userId = computed(() => route.params.userId || user.state.user?.id);

    const isSelf = computed(() => userId.value === user.state.user?.id);

    const {
      friendship,
      friendshipState,
      fetchFriendship,
      onInvite,
      onAccept,
      onDecline,
      onDelete,
      onUnblock
    } = useFriendship();

    const fetchUserDetails = async () => {
      if (userId.value) {
        const result = await userService.getUserDetails(userId.value as string);

        if (result.isOk()) {
          state.user = result.value;
        }
      }
    }

    onMounted(async () => {
      await fetchUserDetails();
      await fetchFriendship();
    });

    const links = computed(() => {
      const result = [
        { name: "profile", label: "Overview" },
        { name: "matches", label: "Matches" },
        { name: "friends", label: "Friends" }
      ];

      return result.map(l => isSelf.value ? l : ({ ...l, params: { userId: state.user?.id }}));
    });

    return {
      isSelf,
      user,
      state,
      links,
      friendship,
      friendshipState,
      onInvite,
      onAccept,
      onDecline,
      onDelete,
      onUnblock,
      FriendshipState: Object.freeze(FriendshipState)
    };
  }
});

export default Profile;
