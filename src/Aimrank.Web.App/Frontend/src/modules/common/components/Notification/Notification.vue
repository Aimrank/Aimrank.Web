<script src="./Notification.ts" lang="ts"></script>
<style src="./Notification.scss" lang="scss" module></style>

<template>
  <div
    :class="{
      [$style.notification]: true,
      [$style.notificationSuccess]: data.color === NotificationColor.Success,
      [$style.notificationWarning]: data.color === NotificationColor.Warning,
      [$style.notificationDanger]: data.color === NotificationColor.Danger
    }"
  >
    <div :class="$style.content">
      {{ data.content }}
      <div
        v-if="data.params"
        :class="$style.buttons"
      >
        <template v-if="data.params.type === 'FRIENDSHIP_INVITATION'">
          <base-button :class="$style.button" small primary @click="onAcceptFriendshipInvitation(data.params.userId)">Accept</base-button>
          <base-button :class="$style.button" small primary @click="onDeclineFriendshipInvitation(data.params.userId)">Decline</base-button>
        </template>
        <template v-else-if="data.params.type === 'LOBBY_INVITATION'">
          <base-button :class="$style.button" small primary @click="onAcceptLobbyInvitation(data.params.lobbyId)">Accept</base-button>
          <base-button :class="$style.button" small primary @click="onCancelLobbyInvitation(data.params.lobbyId)">Decline</base-button>
        </template>
      </div>
    </div>
    <div
      :class="$style.close"
      @click="onClose"
    >
      <icon name="times" />
    </div>
  </div>
</template>
