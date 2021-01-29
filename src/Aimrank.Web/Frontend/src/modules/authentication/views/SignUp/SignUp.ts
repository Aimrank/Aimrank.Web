import { defineComponent, reactive } from "vue";
import { useRouter } from "vue-router";
import { useAuth } from "@/modules/authentication";
import { useResponseErrors } from "@/modules/common/hooks/useResponseErrors";
import BaseButton from "@/modules/common/components/BaseButton";
import FormFieldInput from "@/modules/common/components/FormFieldInput";
import ValidationSummary from "@/modules/common/components/ValidationSummary";

const SignUp = defineComponent({
  components: {
    BaseButton,
    FormFieldInput,
    ValidationSummary
  },
  setup() {
    const state = reactive({
      email: "",
      username: "",
      password: "",
      repeatPassword: ""
    });

    const router = useRouter();
    const errors = useResponseErrors();

    const { signUp } = useAuth();

    const onSubmit = async () => {
      const result = await signUp({
        email: state.email,
        username: state.username,
        password: state.password,
        repeatPassword: state.repeatPassword
      });

      if (result.isOk()) {
        router.push({ name: "home" });
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

export default SignUp;
