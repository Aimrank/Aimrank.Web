import { defineComponent, onMounted, ref } from "vue";
import { steamService, userService } from "~/services";
import { useUser } from "@/user/hooks/useUser";
import { useNotifications } from "@/common/hooks/useNotifications";
import { useInitialState } from "@/common/hooks/useInitialState";
import { IUserDto } from "@/user/models/IUserDto";
import BaseButton from "@/common/components/BaseButton";
import Icon from "@/common/components/Icon";

const Settings = defineComponent({
  components: {
    BaseButton,
    Icon
  },
  setup() {
    const userDetails = ref<IUserDto | null>(null);

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

        if (error) {
          notifications.danger(error);
        }
      }
    });

    const onConnectSteamAccount = async () => {
      const result = await steamService.signInWithSteam();

      if (!result.isOk()) {
        notifications.danger(result.error.title);
      }
    }

    return {
      userDetails,
      onConnectSteamAccount
    };
  }
});

export default Settings;
