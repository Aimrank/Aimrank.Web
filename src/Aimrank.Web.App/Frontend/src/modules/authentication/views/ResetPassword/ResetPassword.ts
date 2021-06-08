import { defineComponent, reactive } from "vue";
import { useRoute, useRouter } from "vue-router";
import { useResetPassword } from "~/graphql/types/types";
import { useNotifications } from "@/common/hooks/useNotifications";
import { useResponseErrors } from "@/common/hooks/useResponseErrors";
import { ErrorResponse } from "@/common/hooks/ErrorResponse";
import Layout from "@/authentication/views/Layout";
import BaseButton from "@/common/components/BaseButton";
import FormFieldInput from "@/common/components/FormFieldInput";
import ValidationSummary from "@/common/components/ValidationSummary";

const ResetPassword = defineComponent({
  components: {
    Layout,
    BaseButton,
    FormFieldInput,
    ValidationSummary,
  },
  setup() {
    const route = useRoute();
    const router = useRouter();

    const state = reactive({
      newPassword: "",
      repeatNewPassword: ""
    });

    const formErrors = useResponseErrors();
    const notifications = useNotifications();

    const { mutate: resetPassword, loading } = useResetPassword();

    const onSubmit = async () => {
      const { errors } = await resetPassword({
        input: {
          token: route.query.token as string,
          userId: route.query.userId as string,
          newPassword: state.newPassword,
          repeatNewPassword: state.repeatNewPassword
        }
      });

      if (errors[0]) {
        formErrors.setErrors(ErrorResponse.fromGraphQLError(errors[0]));
      } else {
        notifications.success("Password reseted successfully");
        router.push("sign-in");
      }
    }

    return {
      state,
      errors: formErrors.state,
      loading,
      onSubmit
    };
  }
});

export default ResetPassword;
