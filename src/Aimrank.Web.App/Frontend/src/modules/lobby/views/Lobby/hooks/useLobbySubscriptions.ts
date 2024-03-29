import { computed, onBeforeUnmount, Ref, watch } from "vue";
import { useRouter } from "vue-router";
import { useAuth } from "@/authentication/hooks/useAuth";
import { useMatchDialog } from "@/lobby/components/MatchDialog/hooks/useMatchDialog";
import { useNotifications } from "@/common/hooks/useNotifications";
import {
  useLobbyConfigurationChanged,
  useLobbyInvitationAccepted,
  useLobbyMemberKicked,
  useLobbyMemberLeft,
  useLobbyMemberRoleChanged,
  useLobbyStatusChanged,
  useMatchAccepted,
  useMatchCanceled,
  useMatchFinished,
  useMatchPlayerLeft,
  useMatchReady,
  useMatchStarted,
  useMatchTimedOut
} from "~/graphql/types/types";
import { GetLobbyQuery } from "~/graphql/types/types";
import { MatchStatus } from "@/profile/models/MatchStatus";
import { LobbyStatus } from "@/lobby/models/LobbyStatus";

export const useLobbySubscriptions = (
  state: Ref<GetLobbyQuery | undefined>
) => {
  const { currentUser } = useAuth();
  const router = useRouter();
  const notifications = useNotifications();

  const { open, close, accept } = useMatchDialog();

  const lobbyInvitationAccepted = useLobbyInvitationAccepted({ lazy: true });
  const lobbyConfigurationChanged = useLobbyConfigurationChanged({ lazy: true });
  const lobbyMemberLeft = useLobbyMemberLeft({ lazy: true });
  const lobbyMemberRoleChanged = useLobbyMemberRoleChanged({ lazy: true });
  const lobbyStatusChanged = useLobbyStatusChanged({ lazy: true });
  const lobbyMemberKicked = useLobbyMemberKicked({ lazy: true });

  const matchReady = useMatchReady({ lazy: true });
  const matchAccepted = useMatchAccepted({ lazy: true });
  const matchStarted = useMatchStarted({ lazy: true });
  const matchCanceled = useMatchCanceled({ lazy: true });
  const matchFinished = useMatchFinished({ lazy: true });
  const matchTimedOut = useMatchTimedOut({ lazy: true });
  const matchPlayerLeft = useMatchPlayerLeft({ lazy: true });

  const unsubscribeAll = () => {
    lobbyInvitationAccepted.unsubscribe();
    lobbyConfigurationChanged.unsubscribe();
    lobbyMemberLeft.unsubscribe();
    lobbyMemberRoleChanged.unsubscribe();
    lobbyStatusChanged.unsubscribe();
    lobbyMemberKicked.unsubscribe();
    
    matchReady.unsubscribe();
    matchAccepted.unsubscribe();
    matchStarted.unsubscribe();
    matchCanceled.unsubscribe();
    matchFinished.unsubscribe();
    matchTimedOut.unsubscribe();
    matchPlayerLeft.unsubscribe();
  }

  const lobbyId = computed(() => state.value?.lobby?.id);

  watch(
    () => lobbyId.value,
    () => {
      unsubscribeAll();

      if (lobbyId.value) {
        lobbyInvitationAccepted.subscribe({ lobbyId: lobbyId.value });
        lobbyConfigurationChanged.subscribe({ lobbyId: lobbyId.value });
        lobbyMemberLeft.subscribe({ lobbyId: lobbyId.value });
        lobbyMemberRoleChanged.subscribe({ lobbyId: lobbyId.value });
        lobbyStatusChanged.subscribe({ lobbyId: lobbyId.value });
        lobbyMemberKicked.subscribe({ lobbyId: lobbyId.value });

        matchReady.subscribe({ lobbyId: lobbyId.value });
        matchAccepted.subscribe({ lobbyId: lobbyId.value });
        matchStarted.subscribe({ lobbyId: lobbyId.value });
        matchCanceled.subscribe({ lobbyId: lobbyId.value });
        matchFinished.subscribe({ lobbyId: lobbyId.value });
        matchTimedOut.subscribe({ lobbyId: lobbyId.value });
        matchPlayerLeft.subscribe({ lobbyId: lobbyId.value });
      }
    }
  );

  onBeforeUnmount(() => unsubscribeAll());

  lobbyConfigurationChanged.onResult(result => {
    const record = result.lobbyConfigurationChanged?.record;
    const lobby = state.value?.lobby;

    if (lobby && record) {
      state.value = {
        ...state.value,
        lobby: {
          ...lobby,
          configuration: {
            ...lobby.configuration,
            maps: record.maps,
            mode: record.mode,
            name: record.name
          }
        }
      };
    }
  });

  lobbyInvitationAccepted.onResult(result => {
    const user = result.lobbyInvitationAccepted?.record?.invitingUser;
    const lobby = state.value?.lobby;

    if (lobby?.members && user) {
      state.value = {
        ...state.value,
        lobby: {
          ...lobby,
          members: [
            ...lobby.members,
            {
              isLeader: false,
              user: {
                id: user.id,
                username: user.username
              }
            }
          ]
        }
      };
    }
  });

  lobbyMemberLeft.onResult(result => {
    const record = result.lobbyMemberLeft?.record;
    const lobby = state.value?.lobby;

    if (lobby?.members && record) {
      state.value = {
        ...state.value,
        lobby: {
          ...lobby,
          members: lobby.members.filter(m => m.user.id !== record.playerId)
        }
      };
    }
  });

  lobbyMemberRoleChanged.onResult(result => {
    const record = result.lobbyMemberRoleChanged?.record;
    const lobby = state.value?.lobby;

    if (lobby?.members && record) {
      state.value = {
        ...state.value,
        lobby: {
          ...lobby,
          members: lobby.members.map(m => {
            if (m.user.id === record.playerId) {
              return {
                ...m,
                isLeader: record.role === 1
              };
            }

            return m;
          })
        }
      }

      if (currentUser.value?.id === record.playerId) {
        notifications.success("You are now lobby leader");
      }
    }
  });

  lobbyStatusChanged.onResult(result => {
    const record = result.lobbyStatusChanged?.record;
    const lobby = state.value?.lobby;

    if (lobby && record) {
      state.value = {
        ...state.value,
        lobby: {
          ...lobby,
          status: record.status
        }
      };
    }
  });

  lobbyMemberKicked.onResult(result => {
    const record = result.lobbyMemberKicked?.record;
    const lobby = state.value?.lobby;

    if (lobby && record) {
      state.value = {
        ...state.value,
        lobby: {
          ...lobby,
          members: lobby.members?.filter(m => m.user.id !== record.playerId)
        }
      };

      if (currentUser.value?.id === record.playerId) {
        notifications.warning("You were kicked from lobby");
        router.replace({ name: "app" });
      }
    }
  });

  matchReady.onResult(result => {
    const record = result.matchReady?.record;
    const lobby = state.value?.lobby;

    if (lobby && record) {
      state.value = {
        ...state.value,
        lobby: {
          ...lobby,
          match: {
            id: record.matchId,
            map: record.map,
            mode: lobby.configuration.mode,
            status: MatchStatus.Ready,
            address: ""
          }
        }
      };

      open(record.matchId, state.value.lobby!.match!.mode, record.expiresAt);
    }
  });

  matchAccepted.onResult(result => {
    if (result.matchAccepted?.record.playerId) {
      accept(result.matchAccepted.record.playerId);
    }
  });

  matchStarted.onResult(result => {
    const record = result.matchStarted?.record;
    const lobby = state.value?.lobby;

    if (lobby?.match && record) {
      state.value = {
        ...state.value,
        lobby: {
          ...lobby,
          match: {
            ...lobby.match,
            map: record.map,
            mode: record.mode,
            address: record.address,
            status: MatchStatus.Started
          }
        }
      };

      close();

      notifications.success(`Server started: ${record.address}`);
    }
  });

  matchCanceled.onResult(() => {
    const lobby = state.value?.lobby;

    if (lobby) {
      state.value = {
        ...state.value,
        lobby: {
          ...lobby,
          match: null,
          status: LobbyStatus.Open
        }
      };
    }

    notifications.danger("Some players failed to connect. Match is canceled.");
  });

  matchFinished.onResult(() => {
    const lobby = state.value?.lobby;

    if (lobby) {
      state.value = {
        ...state.value,
        lobby: {
          ...lobby,
          match: null,
          status: LobbyStatus.Open
        }
      };
    }

    notifications.success("Match finished");
  });

  matchTimedOut.onResult(() => {
    const lobby = state.value?.lobby;

    if (lobby) {
      state.value = {
        ...state.value,
        lobby: {
          ...lobby,
          match: null
        }
      };

      notifications.danger("Some users failed to accept game.");
    }

    close();
  });

  matchPlayerLeft.onResult(result => {
    if (result.matchPlayerLeft?.record.playerId === currentUser.value?.id) {
      notifications.warning("Failed to reconnect. You will be penalized for leaving early.");
    }
  });
}
