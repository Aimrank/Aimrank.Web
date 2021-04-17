import { defineComponent, reactive, watch, ref, onBeforeUnmount } from "vue";
import Icon from "@/common/components/Icon";

const BaseDialog = defineComponent({
  components: {
    Icon
  },
  emits: [
    "click:outside",
    "close"
  ],
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

    const escapeClickEventListener = (event) => {
      if (event.keyCode == 27) {
        emit("close");
      }
    }

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

        document.addEventListener("keyup", escapeClickEventListener);
      } else {
        state.isLeaving = true;
        state.isLeaveActive = true;

        setTimeout(() => {
          state.isVisible = false;
          state.isLeaving = false;
          state.isLeaveActive = false;
        }, 400);

        document.removeEventListener("keyup", escapeClickEventListener);
      }
    });

    onBeforeUnmount(() => {
      document.removeEventListener("keyup", escapeClickEventListener);
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
