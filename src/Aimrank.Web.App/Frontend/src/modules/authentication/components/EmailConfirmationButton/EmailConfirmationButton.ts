import { defineComponent } from "vue";
import { useNotifications } from "@/common/hooks/useNotifications";
import { useRequestEmailConfirmation } from "~/graphql/types/types";
import BaseButton from "@/common/components/BaseButton"

const EmailConfirmationButton = defineComponent({
  components: {
    BaseButton
  },
  props: {
    usernameOrEmail: {
      type: String,
      required: true
    }
  },
  setup(props) {
    const notifications = useNotifications();

    const { mutate: requestEmailConfirmation, loading } = useRequestEmailConfirmation();

    const onResendEmailConfirmation = async () => {
      const { success, errors } = await requestEmailConfirmation({
        input: { usernameOrEmail: props.usernameOrEmail }
      });

      if (success) {
        notifications.success("Confirmation email sent.");
      } else {
        notifications.danger(errors[0].message);
      }
    }

    return {
      loading,
      onResendEmailConfirmation
    };
  }
});

export default EmailConfirmationButton;
