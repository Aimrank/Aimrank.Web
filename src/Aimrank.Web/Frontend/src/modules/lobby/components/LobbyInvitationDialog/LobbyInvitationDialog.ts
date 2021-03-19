import { computed, defineComponent, watch } from "vue";
import { useAuth } from "@/authentication/hooks/useAuth";
import { useNotifications } from "@/common/hooks/useNotifications";
import { useGetFriends, useInviteUserToLobby } from "@/lobby/graphql";
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
    const { currentUser } = useAuth();
    const notifications = useNotifications();
    const { mutate: inviteUserToLobby } = useInviteUserToLobby();
    const { result, fetch } = useGetFriends(currentUser.value!.id);
    
    const users = computed(() => result.value?.user?.friends?.nodes ?? []);

    const close = () => emit("close");

    const onInviteClick = async (userId: string) => {
      const { success, errors } = await inviteUserToLobby({
        lobbyId: props.lobbyId,
        userId
      });

      if (success) {
        close();

        notifications.success("Invitation sent");
      } else {
        notifications.danger(errors[0].message);
      }
    }

    watch(
      () => props.isVisible,
      async () => {
        if (props.isVisible) {
          await fetch();
        }
      }
    );

    return {
      users,
      close,
      onInviteClick
    };
  }
});

export default LobbyInvitationDialog;