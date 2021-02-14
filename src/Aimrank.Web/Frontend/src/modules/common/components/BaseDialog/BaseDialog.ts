import { defineComponent, reactive, watch, ref } from "vue";
import Icon from "@/modules/common/components/Icon";

const BaseDialog = defineComponent({
  components: {
    Icon
  },
  props: {
    visible: {
      type: Boolean,
      required: true
    },
    width: Number,
    minWidth: Number,
    maxWidth: Number,
    hideCloseIcon: Boolean
  },
  setup(props, { emit }) {
    const container = ref<HTMLElement | null>(null);

    const state = reactive({
      isEnterActive: false,
      isLeaveActive: false,
      isEntering: false,
      isLeaving: false,
      isVisible: false
    });

    watch(() => props.visible, async (visible) => {
      if (visible) {
        state.isVisible = true;
        state.isEntering = true;
        state.isEnterActive = true;

        setTimeout(() => {
          state.isEntering = false;
        });

        setTimeout(() => {
          state.isEnterActive = false;
        }, 400);
      } else {
        state.isLeaving = true;
        state.isLeaveActive = true;

        setTimeout(() => {
          state.isVisible = false;
          state.isLeaving = false;
          state.isLeaveActive = false;
        }, 400);
      }
    });

    const onBackgroundClick = (event: any) => {
      if (container.value && !container.value.contains(event.target)) {
        emit("click:outside");
      }
    }

    const onCloseClick = () => {
      emit("close");
    }

    return {
      state,
      container,
      onBackgroundClick,
      onCloseClick
    };
  }
});

export default BaseDialog;
