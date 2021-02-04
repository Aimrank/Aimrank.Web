import { defineComponent } from "vue";
import { useAuth } from "@/modules/authentication";
import { useUser } from "@/modules/user";
import BaseButton from "@/modules/common/components/BaseButton";

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
