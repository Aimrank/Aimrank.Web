import { computed, defineComponent, ref } from "vue";
import { useRouter } from "vue-router";
import { useAuth } from "@/authentication/hooks/useAuth";
import { useNotifications } from "@/common/hooks/useNotifications";
import { useInvitationDialog } from "@/lobby/components/LobbyInvitationDialog/hooks/useLobbyInvitationDialog";
import { useLobbySubscriptions } from "./hooks/useLobbySubscriptions";
import { useCreateLobby, useGetLobby, useKickPlayerFromLobby, useLeaveLobby, useStartSearchingForGame } from "~/graphql/types/types";
import { MatchStatus } from "@/profile/models/MatchStatus";
import BaseButton from "@/common/components/BaseButton";
import LobbyInvitationDialog from "@/lobby/components/LobbyInvitationDialog";
import LobbyConfigurationDialog from "@/lobby/components/LobbyConfigurationDialog";

const maps = {
  aim_map: require("~/assets/images/aim_map.jpg").default,
  am_redline_14: require("~/assets/images/am_redline_14.jpg").default
};

const Lobby = defineComponent({
  components: {
    BaseButton,
    LobbyInvitationDialog,
    LobbyConfigurationDialog
  },
  setup() {
    const configurationDialog = ref();

    const { currentUser } = useAuth();
    const router = useRouter();
    const notifications = useNotifications();

    const { result: state } = useGetLobby();

    const { mutate: createLobby } = useCreateLobby();
    const { mutate: leaveLobby } = useLeaveLobby();
    const { mutate: startSearching } = useStartSearchingForGame();
    const { mutate: kickPlayer } = useKickPlayerFromLobby();

    const lobby = computed(() => state.value?.lobby);
    const members = computed(() => lobby.value?.members ?? []);

    const isCurrentUserLeader = computed(() =>
      state.value?.lobby?.members?.find(m => m?.user?.id === currentUser.value?.id && m?.isLeader));

    useLobbySubscriptions(state);

    const onCreateLobbyClick = async () => {
      const { success, errors, result } = await createLobby();

      if (success) {
        state.value = { ...state.value, lobby: result?.createLobby?.record };
      } else {
        notifications.danger(errors[0].message);
      }
    }

    const onStartSearchingClick = async () => {
      const { success, errors } = await startSearching({ lobbyId: lobby.value?.id });

      if (!success) {
        notifications.danger(errors[0].message);
      }
    }

    const onLeaveLobbyClick = async () => {
      const { success, errors } = await leaveLobby({ lobbyId: state.value?.lobby?.id });

      if (success) {
        router.push({ name: "app" });
      } else {
        notifications.danger(errors[0].message);
      }
    }

    const onKickPlayerClick = async (playerId: string) => {
      const { success, errors } = await kickPlayer({
        lobbyId: state.value?.lobby?.id,
        playerId
      });

      if (!success) {
        notifications.danger(errors[0].message);
      }
    }

    const onChangeMapsClick = () => {
      if (configurationDialog.value) {
        configurationDialog.value.open();
      }
    }

    return {
      maps,
      lobby,
      members,
      isCurrentUserLeader,
      invitationDialog: useInvitationDialog(),
      configurationDialog,
      onCreateLobbyClick,
      onLeaveLobbyClick,
      onStartSearchingClick,
      onKickPlayerClick,
      onChangeMapsClick,
      MatchStatus: Object.freeze(MatchStatus)
    };
  }
});

export default Lobby;
