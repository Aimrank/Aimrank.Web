import { defineComponent, reactive } from "vue";
import { useRouter } from "vue-router";
import { useAuth } from "@/authentication/hooks/useAuth";
import { useResponseErrors } from "@/common/hooks/useGQLResponseErrors";
import { ErrorResponse } from "@/common/models/GErrorResponse";
import BaseButton from "@/common/components/BaseButton";
import FormFieldInput from "@/common/components/FormFieldInput";
import ValidationSummary from "@/common/components/ValidationSummary";

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
      passwordRepeat: ""
    });

    const router = useRouter();
    const errors = useResponseErrors();

    const { signUp } = useAuth();

    const onSubmit = async () => {
      const result = await signUp({
        email: state.email,
        username: state.username,
        password: state.password,
        passwordRepeat: state.passwordRepeat
      });

      if (result.result) {
        router.push({ name: "app" });
      } else {
        errors.setErrors(ErrorResponse.fromGraphQLError(result.errors[0]));
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
