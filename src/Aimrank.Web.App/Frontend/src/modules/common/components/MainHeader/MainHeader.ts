import { defineComponent } from "vue";
import { useAuth } from "@/authentication/hooks/useAuth";
import { useUsersDialog } from "@/common/components/UsersDialog";
import BaseButton from "@/common/components/BaseButton";

const MainHeader = defineComponent({
  components: {
    BaseButton
  },
  setup() {
    const { currentUser, isAuthenticated, signOut } = useAuth();

    const { open } = useUsersDialog();

    return {
      currentUser,
      isAuthenticated,
      open,
      signOut
    };
  }
});

export default MainHeader;
