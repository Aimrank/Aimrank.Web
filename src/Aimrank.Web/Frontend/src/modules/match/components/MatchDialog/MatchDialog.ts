import { computed, defineComponent, onBeforeUnmount, ref, watch } from "vue";
import { matchService } from "@/services";
import { useMatch } from "../../hooks/useMatch";
import { MatchMode } from "../../services/MatchService";
import BaseButton from "@/modules/common/components/BaseButton";
import BaseDialog from "@/modules/common/components/BaseDialog";
import Icon from "@/modules/common/components/Icon";

const MatchDialog = defineComponent({
  components: {
    BaseButton,
    BaseDialog,
    Icon
  },
  setup() {
    const { state, isDialogVisible } = useMatch();

    const { time, start } = useExpirationTime();

    const onAcceptClick = async () => {
      if (state.match) {
        await matchService.accept(state.match?.id);
      }
    }

    const totalAcceptationsNeeded = computed(() => {
      if (!state.match) {
        return 0;
      }

      const map = {
        [MatchMode.OneVsOne]: 2,
        [MatchMode.TwoVsTwo]: 4
      };

      return map[state.match.mode];
    });

    const totalAcceptations = computed(() => state.acceptations);

    watch(
      () => isDialogVisible.value,
      () => {
        if (isDialogVisible.value) {
          start(state.match!.expiresAt!);
        }
      }
    );

    return {
      time,
      totalAcceptationsNeeded,
      totalAcceptations,
      isDialogVisible,
      onAcceptClick
    };
  }
});

const useExpirationTime = () => {
  const time = ref(0);

  let interval: any;

  const getTimeDifference = (t1: string, t2: string) => {
    const a = new Date(t1).getTime();
    const b = new Date(t2).getTime();

    return Math.round((a - b) / 1000);
  }

  onBeforeUnmount(() => clearInterval(interval));

  const start = (expiresAt: string) => {
    if (interval) {
      clearInterval(interval);
    }

    time.value = getTimeDifference(expiresAt, new Date().toUTCString());

    interval = setInterval(
      () => {
        time.value = getTimeDifference(expiresAt, new Date().toUTCString());

        if (time.value <= 0) {
          time.value = 0;

          clearInterval(interval);
        }
      },
      1000
    );
  }

  return { time, start };
}

export default MatchDialog;
