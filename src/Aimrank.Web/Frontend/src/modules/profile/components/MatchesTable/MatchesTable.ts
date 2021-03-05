import { IMatchEntry } from "@/profile/models/MatchEntry";
import { defineComponent } from "vue";

const MatchesTable = defineComponent({
  props: {
    matches: {
      type: Array as () => IMatchEntry[],
      required: true
    }
  }
});

export default MatchesTable;
