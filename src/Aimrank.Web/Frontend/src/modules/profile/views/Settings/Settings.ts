import { defineComponent, onMounted } from "vue";
import { useAuth } from "@/authentication/hooks/useAuth";
import { useInitialState } from "@/common/hooks/useInitialState";
import { useNotifications } from "@/common/hooks/useNotifications";
import { useGetSettingsView } from "~/graphql/types/types";
import { signInWithSteam } from "@/profile/services/signInWithSteam";
import BaseButton from "@/common/components/BaseButton";
import Icon from "@/common/components/Icon";

const Settings = defineComponent({
  components: {
    BaseButton,
    Icon
  },
  setup() {
    const { currentUser } = useAuth();
    const initialState = useInitialState();
    const notifications = useNotifications();

    const { result: state } = useGetSettingsView({ variables: { userId: currentUser.value?.id }});

    onMounted(() => {
      const error = initialState.getError();

      if (error) {
        notifications.danger(error);
      }
    });

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
