import { defineComponent, reactive } from "vue";
import { useChangePassword } from "~/graphql/types/types";
import { useResponseErrors } from "@/common/hooks/useResponseErrors";
import { useNotifications } from "@/common/hooks/useNotifications";
import { ErrorResponse } from "@/common/hooks/ErrorResponse";
import BaseButton from "@/common/components/BaseButton";
import FormFieldInput from "@/common/components/FormFieldInput";
import ValidationSummary from "@/common/components/ValidationSummary";

const ChangePasswordForm = defineComponent({
  components: {
    BaseButton,
    FormFieldInput,
    ValidationSummary
  },
  setup() {
    const notifications = useNotifications();

    const passwordForm = reactive({
      oldPassword: "",
      newPassword: "",
      repeatNewPassword: ""
    });

    const { mutate: changePassword, loading } = useChangePassword();

    const responseErrors = useResponseErrors();

    const onChangePasswordSubmit = async () => {
      const { success, errors } = await changePassword({ input: passwordForm });

      if (success) {
        passwordForm.oldPassword = "";
        passwordForm.newPassword = "";
        passwordForm.repeatNewPassword = "";

        notifications.success("Password changed successfully");
        responseErrors.clearErrors();
      } else {
        responseErrors.setErrors(ErrorResponse.fromGraphQLError(errors[0]));
      }
    }

    return {
      loading,
      passwordForm,
      passwordFormErrors: responseErrors.state,
      onChangePasswordSubmit
    };
  }
});

export default ChangePasswordForm;
