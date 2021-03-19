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
import { useFriendshipInvitationCreated } from "@/profile/graphql";
import { useLobbyInvitationCreated } from "@/lobby/graphql";
import { useNotifications } from "@/common/hooks/useNotifications";
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

    onFriendshipInvitationCreated(result => success("Friendship invitation received"));
    onLobbyInvitationCreated(result => success("Lobby invitation received"));
  }
});

export default AppAuthenticated;
</script>
