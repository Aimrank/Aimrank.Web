import { defineComponent, watch } from "vue";
import { useRouter } from "vue-router";
import { useUsersDialog } from "./hooks/useUsersDialog";
import { debounce } from "@/common/utilities/debounce";
import BaseButton from "@/common/components/BaseButton";
import BaseDialog from "@/common/components/BaseDialog";
import FormFieldInput from "@/common/components/FormFieldInput";

const UsersDialog = defineComponent({
  components: {
    BaseButton,
    BaseDialog,
    FormFieldInput
  },
  setup() {
    const router = useRouter();

    const { state, onFetchResults, onClose } = useUsersDialog();

    const onFetchResultsDebounced = debounce(onFetchResults, 300);

    const onUserClick = (userId: string) => {
      router.push({ name: "profile", params: { userId }});

      onClose();
    }

    watch(() => state.searchQuery, onFetchResultsDebounced);

    return {
      state,
      onUserClick,
      onClose
    };
  }
});

export default UsersDialog;
