import { computed, reactive, readonly } from "vue";
import { MatchMode, MatchStatus } from "@/match/services/MatchService";

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
  acceptations: string[];
}

const state = reactive<IMatchState>({
  match: null,
  acceptations: []
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

  const addAcceptation = (userId: string) => {
    state.acceptations = [ ...state.acceptations, userId ];
  }

  const clearMatch = () => {
    state.match = null;
    state.acceptations = []
  }

  const isDialogVisible = computed(() => state.match?.status === MatchStatus.Ready);

  return {
    state: readonly(state),
    isDialogVisible,
    setMatch,
    setMatchStatus,
    addAcceptation,
    clearMatch
  };
}
