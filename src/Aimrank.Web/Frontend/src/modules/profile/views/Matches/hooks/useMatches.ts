import { reactive } from "vue";
import { matchService } from "~/services";
import { MatchMode } from "@/profile/models/MatchMode";
import { getMatchEntries, IMatchEntry } from "@/profile/models/MatchEntry";

interface IMatchesState {
  isLoading: boolean;
  matches: IMatchEntry[];
  mode: MatchMode;
}

export const useMatches = () => {
  const state = reactive<IMatchesState>({
    isLoading: false,
    matches: [],
    mode: MatchMode.OneVsOne
  });

  const getMatches = async (userId?: string) => {
    if (!userId) {
      return;
    }

    state.isLoading = true;

    const result = await matchService.browse(userId, { mode: state.mode, size: 20 });

    state.isLoading = false;

    if (result.isOk()) {
      state.matches = getMatchEntries(result.value.items, userId);
    }
  }

  return { state, getMatches };
}
