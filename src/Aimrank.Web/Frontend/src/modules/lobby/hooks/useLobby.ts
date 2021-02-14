import { computed, reactive, readonly } from "vue";
import { ILobbyConfiguration, ILobbyDto, ILobbyMember, LobbyStatus } from "../services/LobbyService";

interface ILobbyState {
  lobby: ILobbyDto | null;
}

const state = reactive<ILobbyState>({
  lobby: null
});

export const useLobby = () => {
  const setLobby = (lobby: ILobbyDto | null) => {
    state.lobby = lobby;
  }

  const setLobbyStatus = (status: LobbyStatus) => {
    if (state.lobby) {
      state.lobby = { ...state.lobby, status };
    }
  }

  const setLobbyConfiguration = (configuration: ILobbyConfiguration) => {
    if (state.lobby) {
      state.lobby = { ...state.lobby, configuration };
    }
  }

  const addMember = (member: ILobbyMember) => {
    if (state.lobby) {
      const members = state.lobby.members.filter(m => m.userId !== member.userId);

      state.lobby = { ...state.lobby, members: [...members, member] };
    }
  }

  const removeMember = (userId: string) => {
    if (state.lobby) {
      state.lobby = { ...state.lobby, members: state.lobby.members.filter(m => m.userId != userId) };
    }
  }

  const changeMemberRole = (userId: string, role: number) => {
    if (state.lobby) {
      const members = state.lobby.members.map(m => {
        return {
          userId: m.userId,
          username: m.username,
          isLeader: m.userId === userId ? (role === 1) : m.isLeader
        };
      });

      state.lobby = { ...state.lobby, members };
    }
  }

  const clearLobby = () => {
    state.lobby = null;
  }

  const isAvailable = computed(() => state.lobby !== null);

  return {
    state: readonly(state),
    isAvailable,
    setLobby,
    setLobbyStatus,
    setLobbyConfiguration,
    addMember,
    removeMember,
    changeMemberRole,
    clearLobby
  };
}
