<template>
  <div>
    <main-header />
    <match-dialog />
    <users-dialog />
    <div :class="$style.content">
      <router-view />
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import { NotificationColor, useNotifications } from "@/common/hooks/useNotifications";
import { useFriendshipInvitationCreated, useLobbyInvitationCreated } from "./graphql/types/types";
import MatchDialog from "@/lobby/components/MatchDialog";
import UsersDialog from "@/common/components/UsersDialog";
import MainHeader from "@/common/components/MainHeader";

const AppAuthenticated = defineComponent({
  components: {
    MatchDialog,
    UsersDialog,
    MainHeader,
  },
  setup() {
    const { showNotification } = useNotifications();
    const { onResult: onFriendshipInvitationCreated } = useFriendshipInvitationCreated();
    const { onResult: onLobbyInvitationCreated } = useLobbyInvitationCreated();

    onFriendshipInvitationCreated(result =>
      showNotification({
        content: `${result.friendshipInvitationCreated?.record.invitingUser?.username} sent you friendship invitation`,
        timeout: -1,
        color: NotificationColor.Success,
        params: {
          type: "FRIENDSHIP_INVITATION",
          userId: result.friendshipInvitationCreated?.record.invitingUser?.id
        }
      })
    );

    onLobbyInvitationCreated(result =>
      showNotification({
        content: `${result.lobbyInvitationCreated?.record.invitingUser?.username} invited you to lobby`,
        timeout: -1,
        color: NotificationColor.Success,
        params: {
          type: "LOBBY_INVITATION",
          lobbyId: result.lobbyInvitationCreated?.record.lobbyId
        }
      })
    );
  }
});

export default AppAuthenticated;
</script>

<style lang="scss" module>
.content {
  padding-top: 60px;
}
</style>