import { computed, onBeforeUnmount, Ref, watch } from "vue";
import { useAuth } from "@/authentication/hooks/useAuth";
import { useNotifications } from "@/common/hooks/useNotifications";
import {
  useLobbyConfigurationChanged,
  useLobbyInvitationAccepted,
  useLobbyMemberLeft,
  useLobbyMemberRoleChanged,
  useLobbyStatusChanged
} from "@/lobby/graphql";
import { GetLobbyQuery } from "~/graphql/types/types";

export const useLobbySubscriptions = (
  state: Ref<GetLobbyQuery | undefined>
) => {
  const { currentUser } = useAuth();
  const notifications = useNotifications();

  const lobbyInvitationAccepted = useLobbyInvitationAccepted();
  const lobbyConfigurationChanged = useLobbyConfigurationChanged();
  const lobbyMemberLeft = useLobbyMemberLeft();
  const lobbyMemberRoleChanged = useLobbyMemberRoleChanged();
  const lobbyStatusChanged = useLobbyStatusChanged();

  const unsubscribeAll = () => {
    lobbyInvitationAccepted.unsubscribe();
    lobbyConfigurationChanged.unsubscribe();
    lobbyMemberLeft.unsubscribe();
    lobbyMemberRoleChanged.unsubscribe();
    lobbyStatusChanged.unsubscribe();
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
      }
    }
  );

  onBeforeUnmount(() => unsubscribeAll());

  lobbyConfigurationChanged.onResult(result => {
    if (state.value?.lobby && result.lobbyConfigurationChanged?.record) {
      state.value = {
        ...state.value,
        lobby: {
          ...state.value.lobby,
          configuration: {
            ...state.value.lobby.configuration,
            map: result.lobbyConfigurationChanged.record.map,
            mode: result.lobbyConfigurationChanged.record.mode,
            name: result.lobbyConfigurationChanged.record.name
          }
        }
      };
    }
  });

  lobbyInvitationAccepted.onResult(result => {
    const user = result.lobbyInvitationAccepted?.record?.invitingUser;

    if (state.value?.lobby?.members && user) {
      state.value = {
        ...state.value,
        lobby: {
          ...state.value.lobby,
          members: [
            ...state.value.lobby.members,
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
    if (state.value?.lobby?.members) {
      state.value = {
        ...state.value,
        lobby: {
          ...state.value.lobby,
          members: state.value.lobby.members.filter(m => m.user.id !== result.lobbyMemberLeft?.record.playerId)
        }
      };
    }
  });

  lobbyMemberRoleChanged.onResult(result => {
    if (state.value?.lobby?.members) {
      state.value = {
        ...state.value,
        lobby: {
          ...state.value.lobby,
          members: state.value.lobby.members.map(m => {
            if (m.user.id === result.lobbyMemberRoleChanged?.record.playerId) {
              return {
                ...m,
                isLeader: result.lobbyMemberRoleChanged?.record.role === 1
              };
            }

            return m;
          })
        }
      }

      if (currentUser.value?.id === result.lobbyMemberRoleChanged?.record.playerId) {
        notifications.success("You are now lobby leader");
      }
    }
  });

  lobbyStatusChanged.onResult(result => {
    if (state.value?.lobby && result.lobbyStatusChanged) {
      state.value = {
        ...state.value,
        lobby: {
          ...state.value.lobby,
          status: result.lobbyStatusChanged.record.status
        }
      };
    }
  });
}
