import { defineComponent, onMounted, ref } from "vue";
import { useI18n } from "vue-i18n";
import { useRoute } from "vue-router";
import { useUser } from "@/modules/user";
import { useNotifications } from "@/modules/common/hooks/useNotifications";
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

    const i18n = useI18n();
    const user = useUser();
    const route = useRoute();
    const notifications = useNotifications();

    onMounted(async () => {
      if (!user.state.user) {
        return;
      }

      const result = await userService.getUserDetails(user.state.user.id);

      if (result.isOk()) {
        userDetails.value = result.value;

        if (route.query.error) {
          notifications.danger(i18n.t(`user.views.Settings.errors.${route.query.error}`));
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
