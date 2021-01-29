import { defineComponent } from "vue";
import { useAuth, useUser } from "@/modules/authentication";

const MainHeader = defineComponent({
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
