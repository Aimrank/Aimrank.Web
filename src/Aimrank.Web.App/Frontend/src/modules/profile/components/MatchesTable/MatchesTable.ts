import { computed, defineComponent } from "vue";
import { IMatchEntry } from "@/profile/models/IMatchEntry";

const MatchesTable = defineComponent({
  props: {
    matches: {
      type: Array as () => IMatchEntry[],
      required: true
    }
  },
  setup(props) {
    const matchesFiltered = computed(() => props.matches.filter(m => m.matchPlayerResult));
    return { matchesFiltered };
  }
});

export default MatchesTable;
