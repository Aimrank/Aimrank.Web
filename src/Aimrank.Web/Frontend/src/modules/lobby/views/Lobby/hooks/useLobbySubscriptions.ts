import { computed, onBeforeUnmount, Ref, watch } from "vue";
import { useAuth } from "@/authentication/hooks/useAuth";
import { useMatchDialog } from "@/lobby/components/MatchDialog/hooks/useMatchDialog";
import { useNotifications } from "@/common/hooks/useNotifications";
import {
  useLobbyConfigurationChanged,
  useLobbyInvitationAccepted,
  useLobbyMemberLeft,
  useLobbyMemberRoleChanged,
  useLobbyStatusChanged,
  useMatchAccepted,
  useMatchCanceled,
  useMatchFinished,
  useMatchPlayerLeft,
  useMatchReady,
  useMatchStarted,
  useMatchStarting,
  useMatchTimedOut
} from "@/lobby/graphql";
import { GetLobbyQuery } from "~/graphql/types/types";
import { MatchStatus } from "@/profile/models/MatchStatus";
import { LobbyStatus } from "@/lobby/models/LobbyStatus";

export const useLobbySubscriptions = (
  state: Ref<GetLobbyQuery | undefined>
) => {
  const { currentUser } = useAuth();
  const notifications = useNotifications();

  const { open, close, accept } = useMatchDialog();

  const lobbyInvitationAccepted = useLobbyInvitationAccepted();
  const lobbyConfigurationChanged = useLobbyConfigurationChanged();
  const lobbyMemberLeft = useLobbyMemberLeft();
  const lobbyMemberRoleChanged = useLobbyMemberRoleChanged();
  const lobbyStatusChanged = useLobbyStatusChanged();

  const matchReady = useMatchReady();
  const matchAccepted = useMatchAccepted();
  const matchStarted = useMatchStarted();
  const matchStarting = useMatchStarting();
  const matchCanceled = useMatchCanceled();
  const matchFinished = useMatchFinished();
  const matchTimedOut = useMatchTimedOut();
  const matchPlayerLeft = useMatchPlayerLeft();

  const unsubscribeAll = () => {
    lobbyInvitationAccepted.unsubscribe();
    lobbyConfigurationChanged.unsubscribe();
    lobbyMemberLeft.unsubscribe();
    lobbyMemberRoleChanged.unsubscribe();
    lobbyStatusChanged.unsubscribe();
    
    matchReady.unsubscribe();
    matchAccepted.unsubscribe();
    matchStarted.unsubscribe();
    matchStarting.unsubscribe();
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

        matchReady.subscribe({ lobbyId: lobbyId.value });
        matchAccepted.subscribe({ lobbyId: lobbyId.value });
        matchStarted.subscribe({ lobbyId: lobbyId.value });
        matchStarting.subscribe({ lobbyId: lobbyId.value });
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
            map: record.map,
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
            map: lobby.configuration.map,
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

      notifications.success(`Match created: aimrank.pl${record.address.slice(record.address.indexOf(":"))}`);
    }
  });

  matchStarting.onResult(() => {
    const lobby = state.value?.lobby;

    if (lobby?.match) {
      state.value = {
        ...state.value,
        lobby: {
          ...lobby,
          status: MatchStatus.Starting
        }
      };

      close();

      notifications.success("Starting server...");
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
