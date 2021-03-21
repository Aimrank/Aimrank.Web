import { computed, defineComponent, watch } from "vue";
import { useProfileUser } from "@/profile/hooks/useProfileUser";
import { useProfileStore } from "@/profile/hooks/useProfileStore";
import { useGetProfileView } from "~/graphql/types/types";
import BaseButton from "@/common/components/BaseButton";
import FriendshipButtons from "@/profile/components/FriendshipButtons";


const Profile = defineComponent({
  components: {
    BaseButton,
    FriendshipButtons
  },
  setup() {
    const { profileUserId, isCurrentUserProfile } = useProfileUser();
    const { state, setState } = useProfileStore();
    const { result, fetch } = useGetProfileView({ variables: {userId: profileUserId} });

    const links = computed(() => {
      const result = [
        { name: "profile", label: "Overview" },
        { name: "matches", label: "Matches" },
        { name: "friends", label: "Friends" }
      ];

      return result.map(l => isCurrentUserProfile.value ? l : ({ ...l, params: { userId: profileUserId.value }}));
    });

    watch(
      () => result.value,
      () => setState(result.value)
    );

    watch(
      () => profileUserId.value,
      () => fetch()
    );

    return {
      state,
      links,
      isCurrentUserProfile
    };
  }
});

export default Profile;
