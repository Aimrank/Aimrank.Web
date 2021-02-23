import { computed, defineComponent, onMounted } from "vue";
import { useRouter } from "vue-router";
import { lobbyHub, lobbyService, matchService } from "~/services";
import { useUser } from "@/user/hooks/useUser";
import { useMatch } from "@/match/hooks/useMatch";
import { useLobby } from "@/lobby/hooks/useLobby";
import { useNotifications } from "@/common/hooks/useNotifications";
import { MatchStatus } from "@/match/models/MatchStatus";
import BaseButton from "@/common/components/BaseButton";
import InvitationForm from "@/lobby/components/InvitationForm";
import LobbyConfiguration from "@/lobby/components/LobbyConfiguration";

const maps = {
  aim_map: require("~/assets/images/aim_map.jpg").default,
  am_redline_14: require("~/assets/images/am_redline_14.jpg").default
};

const useLobbyView = () => {
  const user = useUser();
  const lobby = useLobby();
  const match = useMatch();

  const currentUserMembership = computed(() => lobby.state.lobby?.members.find(m => m.userId === user.state.user?.id));

  onMounted(async () => {
    if (!user.state.user) {
      return;
    }

    const result = await lobbyService.getForCurrentUser();

    if (result.isOk() && result.value) {
      lobby.setLobby(result.value);

      await lobbyHub.connect();

      const matchResult = await lobbyService.getMatch(result.value.id);

      if (matchResult.isOk() && matchResult.value) {
        match.setMatch(matchResult.value);
      }
    }
  });

  return {
    lobby,
    match,
    currentUserMembership,
  };
}

const Lobby = defineComponent({
  components: {
    BaseButton,
    InvitationForm,
    LobbyConfiguration
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
      match: computed(() => match.state.match),
      currentUserMembership,
      onStartSearchingClick,
      onLeaveLobbyClick,
      onCreateLobbyClick,
      MatchStatus: Object.freeze(MatchStatus)
    };
  }
});

export default Lobby;
