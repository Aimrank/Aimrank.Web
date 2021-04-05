import { defineComponent, reactive } from "vue";
import { useRoute, useRouter } from "vue-router";
import { useAuth } from "@/authentication/hooks/useAuth";
import { useResponseErrors } from "@/common/hooks/useResponseErrors";
import { ErrorResponse } from "@/common/hooks/ErrorResponse";
import BaseButton from "@/common/components/BaseButton";
import FormFieldInput from "@/common/components/FormFieldInput";
import ValidationSummary from "@/common/components/ValidationSummary";
import EmailConfirmationButton from "@/authentication/components/EmailConfirmationButton";
import RequestPasswordReminderForm from "@/authentication/components/RequestPasswordReminderForm";

const SignIn = defineComponent({
  components: {
    BaseButton,
    FormFieldInput,
    ValidationSummary,
    EmailConfirmationButton,
    RequestPasswordReminderForm
  },
  setup() {
    const state = reactive({
      usernameOrEmail: "",
      password: "",
      emailNotConfirmed: false
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

      if (result.result) {
        if (route.query.returnUrl) {
          await router.push(decodeURIComponent(route.query.returnUrl as string));
        } else {
          await router.push({ name: "app" });
        }
      } else {
        errors.setErrors(ErrorResponse.fromGraphQLError(result.errors[0]));

        state.emailNotConfirmed = result.errors[0].extensions?.code === "email_not_confirmed";
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
