import { defineComponent, onMounted } from "vue";
import { steamService } from "~/services";
import { GetUserQuery, GetUserQueryVariables } from "~/graphql/types/types";
import { useQuery } from "~/graphql/useQuery";
import { useAuth } from "@/authentication/hooks/useAuth";
import { useInitialState } from "@/common/hooks/useInitialState";
import { useNotifications } from "@/common/hooks/useNotifications";
import BaseButton from "@/common/components/BaseButton";
import Icon from "@/common/components/Icon";

import GET_USER from "./Settings.gql";

const Settings = defineComponent({
  components: {
    BaseButton,
    Icon
  },
  setup() {
    const { currentUser } = useAuth();
    const initialState = useInitialState();
    const notifications = useNotifications();

    const { result: state } = useQuery<GetUserQuery, GetUserQueryVariables>({
      query: GET_USER,
      variables: {
        userId: currentUser.value!.id
      }
    });

    onMounted(() => {
      const error = initialState.getError();

      if (error) {
        notifications.danger(error);
      }
    });

    const onConnectSteamAccount = async () => {
      const result = await steamService.signInWithSteam();

      if (!result.isOk()) {
        notifications.danger(result.error.title);
      }
    }

    return {
      state,
      onConnectSteamAccount
    };
  }
});

export default Settings;
