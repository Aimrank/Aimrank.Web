<template>
  <div>
    <main-header />
    <match-dialog />
    <users-dialog />
    <notifications-list />
    <router-view />
  </div>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import { useNotifications } from "@/common/hooks/useNotifications";
import { useFriendshipInvitationCreated, useLobbyInvitationCreated } from "./graphql/types/types";
import MatchDialog from "@/lobby/components/MatchDialog";
import UsersDialog from "@/common/components/UsersDialog";
import MainHeader from "@/common/components/MainHeader";
import NotificationsList from "@/common/components/NotificationsList";

const AppAuthenticated = defineComponent({
  components: {
    MatchDialog,
    UsersDialog,
    MainHeader,
    NotificationsList
  },
  setup() {
    const { success } = useNotifications();
    const { onResult: onFriendshipInvitationCreated } = useFriendshipInvitationCreated();
    const { onResult: onLobbyInvitationCreated } = useLobbyInvitationCreated();

    onFriendshipInvitationCreated(result => success(`${result.friendshipInvitationCreated?.record.invitingUser?.username} sent you friendship invitation`));
    onLobbyInvitationCreated(result => success(`${result.lobbyInvitationCreated?.record.invitingUser?.username} invited you to lobby`));
  }
});

export default AppAuthenticated;
</script>
