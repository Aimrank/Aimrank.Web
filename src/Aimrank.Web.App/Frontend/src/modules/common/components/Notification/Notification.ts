import { defineComponent } from "vue";
import { useRoute, useRouter } from "vue-router";
import { INotification, NotificationColor } from "@/common/hooks/useNotifications";
import {
  useAcceptFriendshipInvitation,
  useAcceptLobbyInvitation,
  useCancelLobbyInvitation,
  useDeclineFriendshipInvitation
} from "~/graphql/types/types";
import BaseButton from "@/common/components/BaseButton";
import Icon from "@/common/components/Icon";

const Notification = defineComponent({
  emits: ["close"],
  components: {
    BaseButton,
    Icon
  },
  props: {
    data: {
      type: Object as () => INotification,
      required: true
    }
  },
  setup(props, { emit }) {
    const route = useRoute();
    const router = useRouter();
    const { mutate: acceptFriendshipInvitation } = useAcceptFriendshipInvitation();
    const { mutate: acceptLobbyInvitation } = useAcceptLobbyInvitation();
    const { mutate: declineFriendshipInvitation } = useDeclineFriendshipInvitation();
    const { mutate: cancelLobbyInvitation } = useCancelLobbyInvitation();

    const onAcceptFriendshipInvitation = async (userId: string) => {
      await acceptFriendshipInvitation({ userId });
      onClose();
    }

    const onDeclineFriendshipInvitation = async (userId: string) => {
      await declineFriendshipInvitation({ userId });
      onClose();
    }

    const onAcceptLobbyInvitation = async (lobbyId: string) => {
      await acceptLobbyInvitation({ lobbyId });
      
      if (route.name === "lobby") {
        router.go(0);
      } else {
        await router.push({ name: "lobby" });
      }

      onClose();
    }

    const onCancelLobbyInvitation = async (lobbyId: string) => {
      await cancelLobbyInvitation({ lobbyId });
      onClose();
    }

    const onClose = () => {
      emit("close");
    }

    return {
      NotificationColor: Object.freeze(NotificationColor),
      onClose,
      onAcceptFriendshipInvitation,
      onAcceptLobbyInvitation,
      onDeclineFriendshipInvitation, 
      onCancelLobbyInvitation
    };
  }
});

export default Notification;
