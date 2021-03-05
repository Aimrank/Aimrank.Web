import { defineComponent, onMounted, computed, watch } from "vue";
import { useRoute } from "vue-router";
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
    const route = useRoute();

    const { state: statsState, getStats } = useStats();
    const { state: matchesState, getMatches } = useMatches();

    const userId = computed(() => route.params.userId || user.state.user?.id);

    watch(
      () => matchesState.mode,
      () => getMatches(userId.value as string)
    );

    onMounted(() => {
      if (userId) {
        getStats(userId.value as string);
        getMatches(userId.value as string);
      }
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