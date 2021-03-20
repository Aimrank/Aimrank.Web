import { computed, defineComponent } from "vue";
import { useRouter } from "vue-router";
import { useAuth } from "@/authentication/hooks/useAuth";
import { useNotifications } from "@/common/hooks/useNotifications";
import { useInvitationDialog } from "@/lobby/components/LobbyInvitationDialog/hooks/useLobbyInvitationDialog";
import { useLobbySubscriptions } from "./hooks/useLobbySubscriptions";
import { useCreateLobby, useGetLobby, useLeaveLobby, useStartSearchingForGame } from "@/lobby/graphql";
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
    const { currentUser } = useAuth();
    const router = useRouter();
    const notifications = useNotifications();

    const { result: state } = useGetLobby();

    const { mutate: createLobby } = useCreateLobby();
    const { mutate: leaveLobby } = useLeaveLobby();
    const { mutate: startSearching } = useStartSearchingForGame();

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

    return {
      maps,
      lobby,
      members,
      isCurrentUserLeader,
      invitationDialog: useInvitationDialog(),
      onCreateLobbyClick,
      onLeaveLobbyClick,
      onStartSearchingClick,
      MatchStatus: Object.freeze(MatchStatus)
    };
  }
});

export default Lobby;
