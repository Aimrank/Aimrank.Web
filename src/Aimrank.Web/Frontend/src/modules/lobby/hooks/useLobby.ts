import { computed, reactive, readonly } from "vue";
import { IMatchDto } from "@/modules/match/services/MatchService";
import { ILobbyConfiguration, ILobbyDto, ILobbyMember, LobbyStatus } from "../services/LobbyService";

interface ILobbyState {
  lobby: ILobbyDto | null;
  match: IMatchDto | null;
}

const state = reactive<ILobbyState>({
  lobby: null,
  match: null
});

export const useLobby = () => {
  const setMatch = (match: IMatchDto | null) => {
    state.match = match;
  }

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
          isLeader: m.userId === userId ? (role === 1) : m.isLeader
        };
      });

      state.lobby = { ...state.lobby, members };
    }
  }

  const clearLobby = () => {
    state.lobby = null;
  }

  const clearMatch = () => {
    if (state.lobby) {
      state.lobby.matchId = null;
    }

    state.match = null;
  }

  const isAvailable = computed(() => state.lobby !== null);

  return {
    state: readonly(state),
    isAvailable,
    setMatch,
    setLobby,
    setLobbyStatus,
    setLobbyConfiguration,
    addMember,
    removeMember,
    changeMemberRole,
    clearLobby,
    clearMatch
  };
}
