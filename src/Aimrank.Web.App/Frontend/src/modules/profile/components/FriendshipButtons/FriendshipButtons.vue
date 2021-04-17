<script src="./FriendshipButtons.ts" lang="ts"></script>
<style src="./FriendshipButtons.scss" lang="scss" module></style>

<template>
  <div :class="$style.buttons">
    <base-button
      v-if="friendshipState === FriendshipState.Blocked"
      :class="$style.button"
      :disabled="!blockingUsersIds.includes(currentUserId)"
      small
      primary
      @click="onUnblock(profileUserId)"
    >
      {{ $t(`profile.components.FriendshipButtons.${blockingUsersIds.includes(currentUserId) ? "unblock" : "blocked"}`) }}
    </base-button>
    <base-button
      v-else-if="friendshipState === FriendshipState.Active"
      :class="$style.button"
      small
      primary
      @click="onDelete(profileUserId)"
    >
      {{ $t("profile.components.FriendshipButtons.removeFriend") }}
    </base-button>
    <template v-else-if="friendshipState === FriendshipState.Pending">
      <base-button
        v-if="invitingUserId === currentUserId"
        :class="$style.button"
        small
        primary
        @click="onDelete(profileUserId)"
      >
        {{ $t("profile.components.FriendshipButtons.removeInvitation") }}
      </base-button>
      <template v-else>
        <base-button :class="$style.button" small primary @click="onAccept(profileUserId)">
          {{ $t("profile.components.FriendshipButtons.accept") }}
        </base-button>
        <base-button :class="$style.button" small primary @click="onDecline(profileUserId)">
          {{ $t("profile.components.FriendshipButtons.decline") }}
        </base-button>
      </template>
    </template>
    <base-button
      v-else
      :class="$style.button"
      small
      primary
      @click="onInvite(profileUserId)"
    >
      {{ $t("profile.components.FriendshipButtons.invite") }}
    </base-button>
  </div>
</template>