import { defineComponent, onMounted, toRef } from "vue";
import { useRouter } from "vue-router";
import { lobbyHub, lobbyService } from "~/services";
import { useNotifications } from "@/common/hooks/useNotifications";
import { useInvitations } from "@/lobby/hooks/useInvitations";
import BaseButton from "@/common/components/BaseButton";

const Invitations = defineComponent({
  components: {
    BaseButton
  },
  setup() {
    const invitations = useInvitations();

    const router = useRouter();
    const notifications = useNotifications();

    onMounted(async () => {
      const result = await lobbyService.getInvitations();

      if (result.isOk()) {
        invitations.setInvitations(result.value);
      }
    });

    const onInvitationAccept = async (lobbyId: string) => {
      const result = await lobbyService.acceptInvitation(lobbyId);

      if (result.isOk()) {
        invitations.removeInvitation(lobbyId);

        lobbyHub.disconnect();

        router.push({ name: "lobby" });
      } else {
        notifications.danger(result.error.title);
      }
    }

    const onInvitationCancel = async (lobbyId: string) => {
      const result = await lobbyService.cancelInvitation(lobbyId);

      if (result.isOk()) {
        invitations.removeInvitation(lobbyId);
      } else {
        notifications.danger(result.error.title);
      }
    }

    return {
      invitations: toRef(invitations.state, "invitations"),
      onInvitationAccept,
      onInvitationCancel
    };
  }
});

export default Invitations;
