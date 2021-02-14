import { defineComponent, ref, watch } from "vue";
import { useI18n } from "vue-i18n";
import { lobbyService, userService } from "~/services";
import { useNotifications } from "@/common/hooks/useNotifications";
import { IUserDetailsDto } from "@/user/services/UserService";
import { debounce } from "@/common/utilities/debounce";
import FormFieldInput from "@/common/components/FormFieldInput";
import Icon from "@/common/components/Icon";

const InvitationForm = defineComponent({
  components: {
    FormFieldInput,
    Icon
  },
  props: {
    lobbyId: {
      type: String,
      required: true
    }
  },
  setup(props) {
    const username = ref("");
    const users = ref<IUserDetailsDto[]>([]);

    const notifications = useNotifications();
    const i18n = useI18n();

    watch(
      () => username.value,
      debounce(async () => {
        const result = await userService.browse(username.value);

        if (result.isOk()) {
          users.value = result.value;
        }
      }, 300)
    );

    const onInviteClick = async (userId: string) => {
      const result = await lobbyService.invite(props.lobbyId, {
        invitedUserId: userId
      });

      if (result.isOk()) {
        notifications.success(i18n.t("lobby.components.InvitationForm.success"));
      } else {
        notifications.danger(result.error.title);
      }
    }

    return {
      username,
      users,
      onInviteClick
    };
  }
});

export default InvitationForm;
