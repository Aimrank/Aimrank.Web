import { defineComponent, onMounted, computed, watch } from "vue";
import { useUser } from "@/profile/hooks/useUser";
import { MatchMode } from "@/profile/models/MatchMode";
import MatchesTable from "@/profile/components/MatchesTable";
import RatingChart from "@/profile/components/RatingChart";
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