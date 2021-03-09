import { ref } from "vue";

export const useInvitationDialog = () => {
  const isVisible = ref(false);

  const onInviteClick = () => {
    isVisible.value = true;
  }

  const onInvitationDialogClose = () => {
    isVisible.value = false;
  }

  return {
    isVisible,
    onInviteClick,
    onInvitationDialogClose
  };
}
