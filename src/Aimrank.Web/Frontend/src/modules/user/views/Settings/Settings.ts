import { defineComponent, onMounted, ref } from "vue";
import { useUser } from "@/modules/user";
import { useNotifications } from "@/modules/common/hooks/useNotifications";
import { useInitialState } from "@/modules/common/hooks/useInitialState";
import { steamService, userService } from "@/services";
import BaseButton from "@/modules/common/components/BaseButton";
import Icon from "@/modules/common/components/Icon";

interface IUserDetails {
  userId: string;
  steamId: string | null;
  username: string;
}

const Settings = defineComponent({
  components: {
    BaseButton,
    Icon
  },
  setup() {
    const userDetails = ref<IUserDetails | null>(null);

    const user = useUser();
    const notifications = useNotifications();
    const { getError } = useInitialState();

    onMounted(async () => {
      if (!user.state.user) {
        return;
      }

      const result = await userService.getUserDetails(user.state.user.id);

      if (result.isOk()) {
        userDetails.value = result.value;

        const error = getError();

        if (error && error.length) {
          notifications.danger(error);
        }
      }
    });

    const onConnectSteamAccount = async () => {
      const result = await steamService.signInWithSteam();

      if (!result.isOk()) {
        alert(result.error.title);
      }
    }

    return {
      userDetails,
      onConnectSteamAccount
    };
  }
});

export default Settings;
