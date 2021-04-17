import { defineComponent, reactive } from "vue";
import { useRequestPasswordReminder } from "~/graphql/types/types";
import { useNotifications } from "@/common/hooks/useNotifications";
import { useResponseErrors } from "@/common/hooks/useResponseErrors";
import { ErrorResponse } from "@/common/hooks/ErrorResponse";
import ValidationSummary from "@/common/components/ValidationSummary";
import FormFieldInput from "@/common/components/FormFieldInput";
import BaseButton from "@/common/components/BaseButton";

const RequestPasswordReminderForm = defineComponent({
  components: {
    ValidationSummary,
    FormFieldInput,
    BaseButton
  },
  setup() {
    const state = reactive({
      usernameOrEmail: ""
    });

    const formErrors = useResponseErrors();
    const notifications = useNotifications();

    const { mutate: requestPasswordReminder, loading } = useRequestPasswordReminder();

    const onSubmit = async () => {
      const { errors } = await requestPasswordReminder({
        input: {
          usernameOrEmail: state.usernameOrEmail
        }
      });

      if (errors[0]) {
        formErrors.setErrors(ErrorResponse.fromGraphQLError(errors[0]));
      } else {
        formErrors.clearErrors();
        notifications.success("Password reminder email sent");
        state.usernameOrEmail = "";
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

export default RequestPasswordReminderForm;
