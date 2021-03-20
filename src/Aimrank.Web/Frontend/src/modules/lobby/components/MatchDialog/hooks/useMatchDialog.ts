import { reactive, readonly } from "vue";
import { MatchMode } from "@/profile/models/MatchMode";

interface IState {
  isVisible: boolean;
  matchId: string;
  matchMode: MatchMode;
  matchAcceptations: string[];
  matcheExpirationTime: string;
}

const state = reactive<IState>({
  isVisible: false,
  matchId: "",
  matchMode: MatchMode.OneVsOne,
  matchAcceptations: [],
  matcheExpirationTime: ""
});

const open = (
  matchId: string,
  matchMode: MatchMode,
  matchExpirationTime: string
) => {
  state.isVisible = true;
  state.matchId = matchId;
  state.matchMode = matchMode;
  state.matchAcceptations = [];
  state.matcheExpirationTime = matchExpirationTime;
}

const close = () => {
  state.isVisible = false;
}

const accept = (userId: string) => {
  if (!state.matchAcceptations.includes(userId)) {
    state.matchAcceptations = [...state.matchAcceptations, userId];
  }
}

export const useMatchDialog = () => ({
  state: readonly(state),
  open,
  close,
  accept
});
