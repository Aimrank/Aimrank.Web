import { defineComponent, onMounted, computed, watch } from "vue";
import { useUser } from "@/user/hooks/useUser";
import { MatchMode } from "@/match/models/MatchMode";
import MatchesTable from "@/match/components/MatchesTable";
import RatingChart from "@/match/components/RatingChart";
import { useMatches } from "./hooks/useMatches";
import { useStats } from "./hooks/useStats";

const Matches = defineComponent({
  components: {
    MatchesTable,
    RatingChart
  },
  setup() {
    const user = useUser();

    const { state: statsState, getStats } = useStats();
    const { state: matchesState, getMatches } = useMatches();

    watch(
      () => matchesState.mode,
      () => getMatches(user.state.user?.id)
    );

    onMounted(() => {
      getStats(user.state.user?.id);
      getMatches(user.state.user?.id);
    });

    const isLoading = computed(() => matchesState.isLoading || statsState.isLoading);

    return {
      isLoading,
      statsState,
      matchesState,
      MatchMode: Object.freeze(MatchMode)
    };
  }
});

export default Matches;