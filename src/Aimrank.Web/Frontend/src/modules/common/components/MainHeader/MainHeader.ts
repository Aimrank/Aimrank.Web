import { defineComponent } from "vue";
import { useAuth } from "@/authentication/hooks/useAuth";
import { useUser } from "@/user/hooks/useUser";
import BaseButton from "@/common/components/BaseButton";

const MainHeader = defineComponent({
  components: {
    BaseButton
  },
  setup() {
    const { state: userState } = useUser();
    const { state: authState, signOut } = useAuth();

    return {
      userState,
      authState,
      signOut
    };
  }
});

export default MainHeader;
