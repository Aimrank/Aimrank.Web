import { defineComponent, onMounted, ref } from "vue";
import { useRouter } from "vue-router";
import { useUser } from "@/modules/user";
import { useNotifications } from "@/modules/common/hooks/useNotifications";
import { lobbyService } from "@/services";
import { ILobbyDto } from "../../services/LobbyService";
import BaseButton from "@/modules/common/components/BaseButton";

const Lobbies = defineComponent({
  components: {
    BaseButton
  },
  setup() {
    const lobbies = ref<ILobbyDto[]>([]);

    const user = useUser();
    const router = useRouter();
    const notifications = useNotifications();

    onMounted(async () => {
      const result = await lobbyService.getList();

      if (result.isOk()) {
        lobbies.value = result.value;
      }
    });

    const onJoinClick = async (id: string) => {
      const result = await lobbyService.join(id);

      if (result.isOk()) {
        router.push({ name: "lobby" });
      } else {
        notifications.danger(result.error.title);
      }
    }

    const onCreateNewLobbyClick = async () => {
      const result = await lobbyService.create();

      if (result.isOk()) {
        router.push({ name: "lobby" });
      } else {
        notifications.danger(result.error.title);
      }
    }

    return {
      lobbies,
      userState: user.state,
      onJoinClick,
      onCreateNewLobbyClick
    };
  }
});

export default Lobbies;
