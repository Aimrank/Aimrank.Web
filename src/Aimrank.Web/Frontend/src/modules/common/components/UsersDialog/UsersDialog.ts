import { computed, defineComponent, ref, watch } from "vue";
import { useRouter } from "vue-router";
import { useGetUsers } from "@/common/graphql";
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

    const username = ref("");

    const { isVisible, close } = useUsersDialog();
    const { result, fetch } = useGetUsers(username);

    const users = computed(() => result.value?.users?.nodes ?? []);

    const fetchUsersDebounced = debounce(() => {
      if (username.value) {
        fetch();
      }
    }, 300);

    watch(
      () => username.value,
      fetchUsersDebounced
    );

    const onUserClick = (userId: string) => {
      router.push({ name: "profile", params: { userId }});
      close();
    }

    return {
      isVisible,
      username,
      users,
      close,
      onUserClick,
    };
  }
});

export default UsersDialog;
