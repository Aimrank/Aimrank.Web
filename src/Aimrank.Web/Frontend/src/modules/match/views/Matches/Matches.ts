import { defineComponent, onMounted, computed, watch, reactive } from "vue";
import { matchService } from "~/services";
import { useUser } from "@/user/hooks/useUser";
import { MatchMode } from "@/match/models/MatchMode";
import { IMatchDto } from "@/match/models/IMatchDto";
import { getMatchEntries } from "@/match/models/MatchEntry";
import MatchesTable from "@/match/components/MatchesTable";
import RatingChart from "@/match/components/RatingChart";

interface IState {
  isLoading: boolean;
  matches: IMatchDto[];
  mode: MatchMode;
}

const Matches = defineComponent({
  components: {
    MatchesTable,
    RatingChart
  },
  setup() {
    const user = useUser();

    const state = reactive<IState>({
      isLoading: false,
      matches: [],
      mode: MatchMode.OneVsOne
    });

    const fetchMatches = async () => {
      if (!user.state.user) {
        return;
      }

      state.isLoading = true;

      const result = await matchService.browse(user.state.user.id, { mode: state.mode, size: 20 });

      state.isLoading = false;

      if (result.isOk()) {
        state.matches = result.value.items;
      }
    }

    watch(
      () => state.mode,
      () => fetchMatches()
    );

    onMounted(() => fetchMatches());

    const matchesWithStatus = computed(() => getMatchEntries(state.matches, user.state.user?.id));

    return {
      state,
      matchesWithStatus,
      MatchMode: Object.freeze(MatchMode)
    };
  }
});

export default Matches;