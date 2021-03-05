import { computed, defineComponent, watch } from "vue";
import { matchService } from "~/services";
import { useMatch } from "@/lobby/hooks/useMatch";
import { useUser } from "@/profile/hooks/useUser";
import { useExpirationTime } from "./helpers/useExpirationTime";
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
    const { state: userState } = useUser();

    const { state: matchState, isDialogVisible } = useMatch();

    const { time, start } = useExpirationTime();

    const onAcceptClick = async () => {
      if (matchState.match) {
        await matchService.accept(matchState.match?.id);
      }
    }

    const totalAcceptationsNeeded = computed(() => {
      if (!matchState.match) {
        return 0;
      }

      const map = {
        [MatchMode.OneVsOne]: 2,
        [MatchMode.TwoVsTwo]: 4
      };

      return map[matchState.match.mode];
    });

    const totalAcceptations = computed(() => matchState.acceptations.length);

    const isAcceptedByUser = computed(() => matchState.acceptations.includes(userState.user!.id));

    watch(
      () => isDialogVisible.value,
      () => {
        if (isDialogVisible.value) {
          start(matchState.match!.expiresAt!);
        }
      }
    );

    return {
      time,
      totalAcceptationsNeeded,
      totalAcceptations,
      isDialogVisible,
      isAcceptedByUser,
      onAcceptClick
    };
  }
});

export default MatchDialog;
