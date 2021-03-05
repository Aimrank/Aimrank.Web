import { computed, defineComponent, onMounted, reactive } from "vue";
import { useRoute } from "vue-router";
import { userService } from "~/services";
import { useUser } from "@/profile/hooks/useUser";
import { IUserDto } from "@/profile/models/IUserDto";
import BaseButton from "@/common/components/BaseButton";

interface IState {
  user: IUserDto | null;
}

const Profile = defineComponent({
  components: {
    BaseButton
  },
  setup() {
    const state = reactive<IState>({
      user: null
    });

    const user = useUser();
    const route = useRoute();

    const userId = computed(() => route.params.userId || user.state.user?.id);

    onMounted(async () => {
      if (userId) {
        const result = await userService.getUserDetails(userId.value as string);

        if (result.isOk()) {
          state.user = result.value;
        }
      }
    });

    const links = computed(() => {
      const isSelf = user.state.user?.id === userId.value;

      const result = [
        { name: "profile", label: "Overview" },
        { name: "matches", label: "Matches" },
        { name: "friends", label: "Friends" }
      ];

      return result.map(l => isSelf ? l : ({ ...l, params: { userId: state.user?.id }}));
    });

    return { state, links };
  }
});

export default Profile;
