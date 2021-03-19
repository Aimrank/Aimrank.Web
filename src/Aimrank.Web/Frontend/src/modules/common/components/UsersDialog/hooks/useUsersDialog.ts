import { ref } from "vue";

const isVisible = ref(false);

const open = () => {
  isVisible.value = true;
}

const close = () => {
  isVisible.value = false;
}

export const useUsersDialog = () => ({
  isVisible,
  open,
  close
});
