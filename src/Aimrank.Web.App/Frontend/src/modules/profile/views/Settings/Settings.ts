import { defineComponent } from "vue";
import { useAuth } from "@/authentication/hooks/useAuth";
import { useNotifications } from "@/common/hooks/useNotifications";
import { useGetSettingsView } from "~/graphql/types/types";
import { signInWithSteam } from "@/profile/services/signInWithSteam";
import ChangePasswordForm from "@/profile/components/ChangePasswordForm";
import BaseButton from "@/common/components/BaseButton";
import Icon from "@/common/components/Icon";

const Settings = defineComponent({
  components: {
    ChangePasswordForm,
    BaseButton,
    Icon
  },
  setup() {
    const { currentUser } = useAuth();
    const notifications = useNotifications();

    const { result: state } = useGetSettingsView({ variables: { userId: currentUser.value?.id }});

    const onConnectSteamAccount = async () => {
      const result = await signInWithSteam();

      if (!result.isOk) {
        notifications.danger(result.error!.title);
      }
    }

    return {
      state,
      onConnectSteamAccount
    };
  }
});

export default Settings;
