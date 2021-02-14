import { computed, reactive, readonly } from "vue";
import { MatchMode, MatchStatus } from "../services/MatchService";

interface IMatch {
  id: string;
  map: string;
  mode: MatchMode;
  status: MatchStatus;
  address: string;
  expiresAt?: string;
}

interface IMatchState {
  match: IMatch | null;
  acceptations: number;
}

const state = reactive<IMatchState>({
  match: null,
  acceptations: 0
});

export const useMatch = () => {
  const setMatch = (match: IMatch | null) => {
    state.match = match;
  }

  const setMatchStatus = (status: MatchStatus) => {
    if (state.match) {
      state.match = { ...state.match, status };
    }
  }

  const incrementAcceptations = () => {
    state.acceptations++;
  }

  const clearMatch = () => {
    state.match = null;
    state.acceptations = 0;
  }

  const isDialogVisible = computed(() => state.match?.status === MatchStatus.Ready);

  return {
    state: readonly(state),
    isDialogVisible,
    setMatch,
    setMatchStatus,
    incrementAcceptations,
    clearMatch
  };
}
