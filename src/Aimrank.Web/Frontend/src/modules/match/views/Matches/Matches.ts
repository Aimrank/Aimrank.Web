import { defineComponent, ref, onMounted, computed } from "vue";
import { matchService } from "@/services";
import { useUser } from "@/modules/user";
import { IMatchHistoryDto } from "../../services/MatchService";

const getMatchResult = (match: IMatchHistoryDto, userId?: string) => {
  const p1 = match.teamTerrorists[0];
  const p2 = match.teamCounterTerrorists[0];

  if (p1.score == p2.score) {
    return 0;
  }

  const winner = p1.score > p2.score ? p1.id : p2.id;

  return winner === userId ? 1 : -1;
}

const Matches = defineComponent({
  setup() {
    const user = useUser();

    const matches = ref<IMatchHistoryDto[]>([]);

    onMounted(async () => {
      if (!user.state.user) {
        return;
      }

      const result = await matchService.browse(user.state.user.id);

      if (result.isOk()) {
        matches.value = result.value;
      }
    });

    const matchesWithStatus = computed(() => matches.value.map(m =>
      ({ ...m, matchResult: getMatchResult(m, user.state.user?.id) })
    ));

    return { matchesWithStatus };
  }
});

export default Matches;