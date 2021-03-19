import { defineComponent, computed, watch, ref } from "vue";
import { useRoute } from "vue-router";
import { useAuth } from "@/authentication/hooks/useAuth";
import { useMatches, useMatchesView } from "@/profile/graphql";
import { MatchMode } from "@/profile/models/MatchMode";
import { MatchWinner } from "@/profile/models/MatchWinner";
import MatchesTable from "@/profile/components/MatchesTable";
import RatingChart from "@/profile/components/RatingChart";

const Matches = defineComponent({
  components: {
    MatchesTable,
    RatingChart
  },
  setup() {
    const auth = useAuth();
    const route = useRoute();

    const mode = ref<MatchMode>(MatchMode.OneVsOne);

    const userId = computed(() => route.params.userId as string || auth.state.user!.id);

    const { result: state, loading: isStateLoading } = useMatchesView(userId.value, mode.value);
    const { result, fetch, loading: isMatchLoading } = useMatches(userId, mode);

    watch(
      () => mode.value,
      () => fetch()
    );

    watch(
      () => result.value,
      () => state.value = { ...state.value, matches: result.value?.matches }
    )

    const isLoading = computed(() => isStateLoading.value || isMatchLoading.value);

    const stats = computed(() => state.value?.user?.stats);

    const matches = computed(() => state.value?.matches?.nodes?.map(match => {
      return {
        ...match,
        matchResult: getMatchResult(match, userId.value),
        matchPlayerResult: getMatchPlayerResult(match, userId.value)
      }
    }) ?? []);

    return {
      mode,
      stats,
      matches,
      isLoading,
      MatchMode: Object.freeze(MatchMode)
    };
  }
});

const getMatchResult = (match: any, userId?: string) => {
  if (match.winner === MatchWinner.Draw) {
    return 0;
  }

  if (match.winner === MatchWinner.T && match.teamTerrorists.some(m => m.user.id === userId) ||
      match.winner === MatchWinner.CT && match.teamCounterTerrorists.some(m => m.user.id === userId))
  {
    return 1;
  }

  return -1;
}

const getMatchPlayerResult = (match: any, userId?: string) => {
  const player = [...match.teamTerrorists, ...match.teamCounterTerrorists].find(p => p.user.id === userId);

  if (player) {
    return {
      kills: player.kills,
      deaths: player.deaths,
      rating: player.ratingEnd,
      difference: player.ratingEnd - player.ratingStart,
      hsPercentage: player.kills === 0 ? 0 : player.hs / player.kills * 100
    };
  }

  return null;
}

export default Matches;