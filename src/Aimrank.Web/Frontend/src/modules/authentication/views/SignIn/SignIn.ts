import { defineComponent, reactive } from "vue";
import { useRoute, useRouter } from "vue-router";
import { useAuth } from "@/authentication/hooks/useAuth";
import { useResponseErrors } from "@/common/hooks/useResponseErrors";
import BaseButton from "@/common/components/BaseButton";
import FormFieldInput from "@/common/components/FormFieldInput";
import ValidationSummary from "@/common/components/ValidationSummary";

const SignIn = defineComponent({
  components: {
    BaseButton,
    FormFieldInput,
    ValidationSummary
  },
  setup() {
    const state = reactive({
      usernameOrEmail: "",
      password: ""
    });

    const route = useRoute();
    const router = useRouter();
    const errors = useResponseErrors();
    
    const { signIn } = useAuth();

    const onSubmit = async () => {
      const result = await signIn({
        usernameOrEmail: state.usernameOrEmail,
        password: state.password
      });

      if (result.isOk()) {
        if (route.query.returnUrl) {
          await router.push(decodeURIComponent(route.query.returnUrl as string));
        } else {
          await router.push({ name: "home" });
        }
      } else {
        errors.setErrors(result.error);
      }
    }

    return {
      state,
      errors: errors.state,
      onSubmit
    };
  }
});

export default SignIn;
