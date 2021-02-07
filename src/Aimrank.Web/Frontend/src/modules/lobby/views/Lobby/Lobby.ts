import { computed, defineComponent, onMounted, ref } from "vue";
import { useRouter } from "vue-router";
import { useUser } from "@/modules/user";
import { useNotifications } from "@/modules/common/hooks/useNotifications";
import { lobbyHub, lobbyService, matchService } from "@/services";
import { ILobbyDto } from "../../services/LobbyService";
import { IMatchDto } from "@/modules/match/services/MatchService";
import BaseButton from "@/modules/common/components/BaseButton";
import FormFieldInput from "@/modules/common/components/FormFieldInput";

const useLobby = () => {
  const user = useUser();

  const lobby = ref<ILobbyDto | null>();
  const match = ref<IMatchDto | null>();
  const map = ref("");
  const inviteUserId = ref("");
  
  const member = computed(() => lobby.value?.members.find(m => m.userId === user.state.user?.id));

  onMounted(async () => {
    if (!user.state.user) {
      return;
    }

    const result = await lobbyService.getForCurrentUser();

    if (result.isOk()) {
      lobby.value = result.value;
      map.value = result.value.configuration.map;

      await lobbyHub.connect();

      if (lobby.value.matchId) {
        const matchResult = await matchService.getById(lobby.value.matchId);

        if (matchResult.isOk()) {
          match.value = matchResult.value;
        }
      }
    }
  });

  return {
    member,
    lobby,
    match,
    map,
    inviteUserId
  };
}

const Lobby = defineComponent({
  components: {
    BaseButton,
    FormFieldInput
  },
  setup() {
    const { member, lobby, match, map, inviteUserId } = useLobby();

    const router = useRouter();
    const notifications = useNotifications();

    const onStartSearchingClick = async () => {
      if (!lobby.value) {
        return;
      }

      const result = await lobbyService.startSearching(lobby.value.id);

      if (result.isOk()) {
        notifications.success("Searching for available server...");
      } else {
        notifications.danger(result.error.title);
      }
    }

    const onChangeConfigurationClick = async () => {
      if (!lobby.value) {
        return;
      }

      const result = await lobbyService.changeConfiguration(lobby.value.id, {
        map: map.value,
        name: lobby.value.configuration.name,
        mode: lobby.value.configuration.mode
      });

      if (result.isOk()) {
        lobby.value = {
          ...lobby.value,
          configuration: {
            ...lobby.value.configuration,
            map: map.value
          }
        };
      } else {
        notifications.danger(result.error.title);
      }
    }

    const onLeaveLobbyClick = async () => {
      if (!lobby.value) {
        return;
      }

      const result = await lobbyService.leave(lobby.value.id);

      if (result.isOk()) {
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
          lobby.value = lobbyResult.value;
          map.value = lobbyResult.value.configuration.map;
        }
      } else {
        notifications.danger(result.error.title);
      }
    }

    const onInviteUserClick = async () => {
      if (!lobby.value) {
        return;
      }

      const result = await lobbyService.invite(lobby.value.id, { invitedUserId: inviteUserId.value });

      if (result.isOk()) {
        notifications.success("User invited");
      } else {
        notifications.danger(result.error.title);
      }
    }

    return {
      map,
      lobby,
      match,
      member,
      inviteUserId,
      onStartSearchingClick,
      onChangeConfigurationClick,
      onLeaveLobbyClick,
      onCreateLobbyClick,
      onInviteUserClick
    };
  }
});

export default Lobby;
