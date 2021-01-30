import { defineComponent, onMounted, ref } from "vue";
import { steamService, userService } from "@/services";
import { useUser } from "@/modules/user";

interface IUserDetails {
  userId: string;
  steamId: string | null;
  username: string;
}

const Settings = defineComponent({
  setup() {
    const userDetails = ref<IUserDetails | null>(null);

    const user = useUser();

    onMounted(async () => {
      if (!user.state.user) {
        return;
      }

      const result = await userService.getUserDetails(user.state.user.id);

      if (result.isOk()) {
        userDetails.value = result.value;
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
