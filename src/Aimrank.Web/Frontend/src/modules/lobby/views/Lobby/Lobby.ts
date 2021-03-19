import { computed, defineComponent } from "vue";
import { useRouter } from "vue-router";
import { lobbyHub, lobbyService } from "~/services";
import { useNotifications } from "@/common/hooks/useNotifications";
import { useLobbyView } from "./hooks/useLobbyView";
import { useInvitationDialog } from "./hooks/useLobbyInvitationDialog";
import { MatchStatus } from "@/profile/models/MatchStatus";
import BaseButton from "@/common/components/BaseButton";
import LobbyConfiguration from "@/lobby/components/LobbyConfiguration";
import LobbyInvitationDialog from "@/lobby/components/LobbyInvitationDialog";

const maps = {
  aim_map: require("~/assets/images/aim_map.jpg").default,
  am_redline_14: require("~/assets/images/am_redline_14.jpg").default
};

const Lobby = defineComponent({
  components: {
    BaseButton,
    LobbyConfiguration,
    LobbyInvitationDialog
  },
  setup() {
    const { lobby, match, currentUserMembership } = useLobbyView();

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

    const onLeaveLobbyClick = async () => {
      if (!lobby.isAvailable) {
        return;
      }

      const result = await lobbyService.leave(lobby.state.lobby!.id);

      if (result.isOk()) {
        lobbyHub.disconnect();
        lobby.clearLobby();
        match.clearMatch();

        router.push({ name: "app" });
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
      match: computed(() => match.state.match),
      invitationDialog: useInvitationDialog(),
      currentUserMembership,
      onStartSearchingClick,
      onLeaveLobbyClick,
      onCreateLobbyClick,
      MatchStatus: Object.freeze(MatchStatus)
    };
  }
});

export default Lobby;
