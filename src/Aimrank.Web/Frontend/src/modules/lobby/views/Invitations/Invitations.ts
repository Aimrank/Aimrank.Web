import { defineComponent, onMounted, ref } from "vue";
import { useRouter } from "vue-router";
import { useNotifications } from "@/modules/common/hooks/useNotifications";
import { lobbyHub, lobbyService } from "@/services";
import { ILobbyInvitationDto } from "../../services/LobbyService";
import BaseButton from "@/modules/common/components/BaseButton";

const Invitations = defineComponent({
  components: {
    BaseButton
  },
  setup() {
    const invitations = ref<ILobbyInvitationDto[]>([]);

    const router = useRouter();
    const notifications = useNotifications();

    onMounted(async () => {
      const result = await lobbyService.getInvitations();

      if (result.isOk()) {
        invitations.value = result.value;
      }
    });

    const onInvitationAccept = async (lobbyId: string) => {
      const result = await lobbyService.acceptInvitation(lobbyId);

      if (result.isOk()) {
        lobbyHub.disconnect();

        router.push({ name: "lobby" });
      } else {
        notifications.danger(result.error.title);
      }
    }

    const onInvitationCancel = async (lobbyId: string) => {
      const result = await lobbyService.cancelInvitation(lobbyId);

      if (result.isOk()) {
        invitations.value = invitations.value.filter(i => i.lobbyId !== lobbyId);
      } else {
        notifications.danger(result.error.title);
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
