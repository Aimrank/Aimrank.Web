import { defineComponent } from "vue";
import { useAuth } from "@/modules/authentication";
import { useUser } from "@/modules/user";

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
