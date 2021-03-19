import { ref } from "vue";

export const useInvitationDialog = () => {
  const isVisible = ref(false);

  const open = () => {
    isVisible.value = true;
  }

  const close = () => {
    isVisible.value = false;
  }

  return {
    isVisible,
    open,
    close
  };
}
