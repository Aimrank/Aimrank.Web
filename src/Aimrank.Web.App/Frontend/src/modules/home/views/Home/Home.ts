import { defineComponent } from "vue";
import { useAuth } from "@/authentication/hooks/useAuth";
import BaseButton from "@/common/components/BaseButton";

const Home = defineComponent({
  components: {
    BaseButton
  },
  setup() {
    const { isAuthenticated, currentUser } = useAuth();
    return { isAuthenticated, currentUser };
  }
});

export default Home;