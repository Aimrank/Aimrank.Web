import { computed, defineComponent, onMounted, ref } from "vue";
import { useRouter } from "vue-router";
import { useUser } from "@/modules/user";
import { useNotifications } from "@/modules/common/hooks/useNotifications";
import { useLobby } from "../../hooks/useLobby";
import { lobbyHub, lobbyService, matchService } from "@/services";
import BaseButton from "@/modules/common/components/BaseButton";
import FormFieldInput from "@/modules/common/components/FormFieldInput";
import InvitationForm from "../../components/InvitationForm";

const useLobbyView = () => {
  const user = useUser();
  const lobby = useLobby();

  const map = ref("");
  
  const currentUserMembership = computed(() => lobby.state.lobby?.members.find(m => m.userId === user.state.user?.id));

  onMounted(async () => {
    if (!user.state.user) {
      return;
    }

    const result = await lobbyService.getForCurrentUser();

    if (result.isOk()) {
      lobby.setLobby(result.value);

      map.value = result.value.configuration.map;

      await lobbyHub.connect();

      if (result.value.matchId) {
        const matchResult = await matchService.getById(result.value.matchId);

        if (matchResult.isOk()) {
          lobby.setMatch(matchResult.value);
        }
      }
    }
  });

  return {
    map,
    lobby,
    currentUserMembership,
  };
}

const Lobby = defineComponent({
  components: {
    BaseButton,
    FormFieldInput,
    InvitationForm
  },
  setup() {
    const { map, lobby, currentUserMembership } = useLobbyView();

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

    const onChangeConfigurationClick = async () => {
      if (!lobby.isAvailable) {
        return;
      }

      const result = await lobbyService.changeConfiguration(lobby.state.lobby!.id, {
        map: map.value,
        name: lobby.state.lobby!.configuration.name,
        mode: lobby.state.lobby!.configuration.mode
      });

      if (result.isOk()) {
        lobby.setLobbyConfiguration({ ...lobby.state.lobby!.configuration, map: map.value });
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

        if (lobbyResult.isOk()) {
          lobby.setLobby(lobbyResult.value);

          map.value = lobbyResult.value.configuration.map;
        }
      } else {
        notifications.danger(result.error.title);
      }
    }

    return {
      map,
      lobby: computed(() => lobby.state.lobby),
      match: computed(() => lobby.state.match),
      currentUserMembership,
      onStartSearchingClick,
      onChangeConfigurationClick,
      onLeaveLobbyClick,
      onCreateLobbyClick
    };
  }
});

export default Lobby;
