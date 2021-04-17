import { computed, defineComponent, watch } from "vue";
import { useExpirationTime } from "./hooks/useExpirationTime";
import { useMatchDialog } from "./hooks/useMatchDialog";
import { useAcceptMatch } from "~/graphql/types/types";
import { useAuth } from "@/authentication/hooks/useAuth";
import { MatchMode } from "@/profile/models/MatchMode";
import BaseButton from "@/common/components/BaseButton";
import BaseDialog from "@/common/components/BaseDialog";
import Icon from "@/common/components/Icon";

const MatchDialog = defineComponent({
  components: {
    BaseButton,
    BaseDialog,
    Icon
  },
  setup() {
    const { currentUser } = useAuth();
    const { state } = useMatchDialog();

    const { mutate: acceptMatch } = useAcceptMatch();

    const onAcceptClick = async () => await acceptMatch({ matchId: state.matchId });

    const totalAcceptationsNeeded = computed(() => {
      const map = {
        [MatchMode.OneVsOne]: 2,
        [MatchMode.TwoVsTwo]: 4
      };

      return map[state.matchMode];
    });

    const totalAcceptations = computed(() => state.matchAcceptations.length);
    const isAcceptedByUser = computed(() => state.matchAcceptations.includes(currentUser.value!.id));

    const { time, start } = useExpirationTime();

    watch(
      () => state.isVisible,
      () => {
        if (state.isVisible) {
          start(state.matcheExpirationTime);
        }
      }
    );

    return {
      time,
      state,
      totalAcceptationsNeeded,
      totalAcceptations,
      isAcceptedByUser,
      onAcceptClick
    };
  }
});

export default MatchDialog;
