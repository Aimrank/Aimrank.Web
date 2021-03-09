import { defineComponent, ref, watch } from "vue";
import { friendshipService, lobbyService } from "~/services";
import { useUser } from "@/profile/hooks/useUser";
import { IUserDto } from "@/profile/models/IUserDto";
import { useNotifications } from "@/common/hooks/useNotifications";
import BaseButton from "@/common/components/BaseButton";
import BaseDialog from "@/common/components/BaseDialog";

const LobbyInvitationDialog = defineComponent({
  components: {
    BaseButton,
    BaseDialog
  },
  emits: ["close"],
  props: {
    isVisible: Boolean,
    lobbyId: {
      type: String,
      required: true
    }
  },
  setup(props, { emit }) {
    const user = useUser();
    const users = ref<IUserDto[]>([]);

    const notifications = useNotifications();

    const close = () => emit("close");

    const onDialogOpen = async () => {
      const result = await friendshipService.getFriendsList(user.state.user!.id);

      if (result.isOk()) {
        users.value = result.value;
      }
    }

    const onInviteClick = async (userId: string) => {
      const result = await lobbyService.invite(props.lobbyId, { invitedUserId: userId });

      if (result.isOk()) {
        close();

        notifications.success("Invitation sent");
      } else {
        notifications.danger(result.error.title);
      }
    }

    watch(() => props.isVisible, onDialogOpen);

    return {
      users,
      close,
      onInviteClick
    };
  }
});

export default LobbyInvitationDialog;