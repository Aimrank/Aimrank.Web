import { reactive } from "vue"
import { statsService } from "~/services";
import { IUserStatsDto } from "@/match/models/IUserStatsDto"

interface IStatsState {
  isLoading: boolean;
  stats: IUserStatsDto | null;
}

export const useStats = () => {
  const state = reactive<IStatsState>({
    isLoading: false,
    stats: null
  });

  const getStats = async (userId?: string) => {
    if (!userId) {
      return;
    }

    state.isLoading = true;

    const result = await statsService.getStats(userId);

    state.isLoading = false;

    if (result.isOk()) {
      state.stats = result.value;
    }
  }

  return { state, getStats };
}
