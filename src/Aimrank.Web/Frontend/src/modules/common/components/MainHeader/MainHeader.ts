import { defineComponent } from "vue";
import { useAuth } from "@/authentication/hooks/useAuth";
import { useUser } from "@/profile/hooks/useUser";
import { useUsersDialog } from "@/common/components/UsersDialog";
import BaseButton from "@/common/components/BaseButton";

const MainHeader = defineComponent({
  components: {
    BaseButton
  },
  setup() {
    const { state: userState } = useUser();
    const { state: authState, signOut } = useAuth();

    const { open } = useUsersDialog();

    return {
      userState,
      authState,
      open,
      signOut
    };
  }
});

export default MainHeader;
