import { defineComponent } from "vue";
import { useRoute } from "vue-router";
import EmailConfirmationButton from "@/authentication/components/EmailConfirmationButton";

const SignUpSuccess = defineComponent({
  components: {
    EmailConfirmationButton
  },
  setup() {
    const route = useRoute();
    return { route };
  }
});

export default SignUpSuccess;