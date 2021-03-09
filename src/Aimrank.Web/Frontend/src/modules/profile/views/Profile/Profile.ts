import { computed, defineComponent, onMounted, watch } from "vue";
import { useRoute } from "vue-router";
import { useUser } from "@/profile/hooks/useUser";
import { useProfile } from "@/profile/hooks/useProfile";
import BaseButton from "@/common/components/BaseButton";
import FriendshipButtons from "@/profile/components/FriendshipButtons";

const Profile = defineComponent({
  components: {
    BaseButton,
    FriendshipButtons
  },
  setup() {
    const user = useUser();
    const route = useRoute();

    const { state, fetchPage } = useProfile();

    const userId = computed(() => route.params.userId as string || user.state.user!.id);
    const currentUserId = computed(() => user.state.user!.id);

    const links = computed(() => {
      const result = [
        { name: "profile", label: "Overview" },
        { name: "matches", label: "Matches" },
        { name: "friends", label: "Friends" }
      ];

      return result.map(l => userId.value === currentUserId.value ? l : ({ ...l, params: { userId: state.user?.id }}));
    });

    watch(
      () => userId.value,
      () => fetchPage(userId.value, user.state.user!.id)
    );

    onMounted(() => fetchPage(userId.value, user.state.user!.id));

    return {
      userId,
      currentUserId,
      state,
      links
    };
  }
});

export default Profile;
