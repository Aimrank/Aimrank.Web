import { computed, defineComponent } from "vue";
import { useRouter } from "vue-router";
import { useNotifications } from "@/common/hooks/useNotifications";
import { useAcceptLobbyInvitation, useCancelLobbyInvitation, useGetLobbyInvitations } from "~/graphql/types/types";
import BaseButton from "@/common/components/BaseButton";

const Invitations = defineComponent({
  components: {
    BaseButton
  },
  setup() {
    const router = useRouter();
    const notifications = useNotifications();

    const { result: state } = useGetLobbyInvitations();
    const { mutate: acceptInvitation } = useAcceptLobbyInvitation();
    const { mutate: cancelInvitation } = useCancelLobbyInvitation();

    const invitations = computed(() => state.value?.lobbyInvitations ?? []);

    const onInvitationAccept = async (lobbyId: string) => {
      const { success, errors } = await acceptInvitation({ lobbyId });

      if (success) {
        state.value = {
          ...state.value,
          lobbyInvitations: state.value?.lobbyInvitations?.filter(i => i?.lobbyId !== lobbyId)
        };

        router.push({ name: "lobby" });
      } else {
        notifications.danger(errors[0].message);
      }
    }

    const onInvitationCancel = async (lobbyId: string) => {
      const { success, errors } = await cancelInvitation({ lobbyId });

      if (success) {
        state.value = {
          ...state.value,
          lobbyInvitations: state.value?.lobbyInvitations?.filter(i => i?.lobbyId !== lobbyId)
        };
      } else {
        notifications.danger(errors[0].message);
      }
    }

    return {
      invitations,
      onInvitationAccept,
      onInvitationCancel
    };
  }
});

export default Invitations;
