import { computed, defineComponent, onMounted, ref } from "vue";
import { useRouter } from "vue-router";
import { useUser } from "@/modules/user";
import { useNotifications } from "@/modules/common/hooks/useNotifications";
import { useLobby } from "../../hooks/useLobby";
import { lobbyHub, lobbyService, matchService } from "@/services";
import BaseButton from "@/modules/common/components/BaseButton";
import InvitationForm from "../../components/InvitationForm";
import MapButton from "../../components/MapButton";

const maps = {
  aim_map: require("@/assets/images/aim_map.jpg").default,
  am_redline_14: require("@/assets/images/am_redline_14.jpg").default
};

const useLobbyView = () => {
  const user = useUser();
  const lobby = useLobby();

  const currentUserMembership = computed(() => lobby.state.lobby?.members.find(m => m.userId === user.state.user?.id));

  onMounted(async () => {
    if (!user.state.user) {
      return;
    }

    const result = await lobbyService.getForCurrentUser();

    if (result.isOk() && result.value) {
      lobby.setLobby(result.value);

      await lobbyHub.connect();

      const matchResult = await matchService.getByLobbyId(result.value.id);

      if (matchResult.isOk() && matchResult.value) {
        lobby.setMatch(matchResult.value);
      }
    }
  });

  return {
    lobby,
    currentUserMembership,
  };
}

const Lobby = defineComponent({
  components: {
    BaseButton,
    InvitationForm,
    MapButton
  },
  setup() {
    const { lobby, currentUserMembership } = useLobbyView();

    const router = useRouter();
    const notifications = useNotifications();

    const onStartSearchingClick = async () => {
      if (!lobby.isAvailable) {
        return;
      }

      const result = await lobbyService.startSearching(lobby.state.lobby!.id);

      if (!result.isOk()) {
        notifications.danger(result.error.title);
      }
    }

    const onChangeMapClick = async (name: string) => {
      if (!lobby.isAvailable || name === lobby.state.lobby?.configuration.map) {
        return;
      }

      const result = await lobbyService.changeConfiguration(lobby.state.lobby!.id, {
        map: name,
        name: lobby.state.lobby!.configuration.name,
        mode: lobby.state.lobby!.configuration.mode
      });

      if (result.isOk()) {
        lobby.setLobbyConfiguration({ ...lobby.state.lobby!.configuration, map: name });
      } else {
        notifications.danger(result.error.title);
      }
    }

    const onLeaveLobbyClick = async () => {
      if (!lobby.isAvailable) {
        return;
      }

      const result = await lobbyService.leave(lobby.state.lobby!.id);

      if (result.isOk()) {
        lobbyHub.disconnect();
        lobby.clearLobby();
        lobby.clearMatch();

        router.push({ name: "home" });
      } else {
        notifications.danger(result.error.title);
      }
    }

    const onCreateLobbyClick = async () => {
      const result = await lobbyService.create();

      if (result.isOk()) {
        const lobbyResult = await lobbyService.getForCurrentUser();

        if (lobbyResult.isOk() && lobbyResult.value) {
          lobby.setLobby(lobbyResult.value);
          lobbyHub.connect();
        }
      } else {
        notifications.danger(result.error.title);
      }
    }

    return {
      maps,
      lobby: computed(() => lobby.state.lobby),
      match: computed(() => lobby.state.match),
      currentUserMembership,
      onStartSearchingClick,
      onLeaveLobbyClick,
      onCreateLobbyClick,
      onChangeMapClick
    };
  }
});

export default Lobby;
