<script src="./FriendshipButtons.ts" lang="ts"></script>
<style src="./FriendshipButtons.scss" lang="scss" module></style>

<template>
  <div
    v-if="state.user && !state.isLoading"
    :class="$style.buttons"
  >
    <base-button
      v-if="state.friendship && friendshipState === FriendshipState.Blocked"
      :class="$style.button"
      :disabled="!state.friendship.blockingUsersIds.includes(currentUserId)"
      small
      primary
      @click="unblockFriend(userId, currentUserId)"
    >
      {{ $t(`profile.components.FriendshipButtons.${state.friendship.blockingUsersIds.includes(currentUserId) ? "unblock" : "blocked"}`) }}
    </base-button>
    <base-button
      v-else-if="friendshipState === FriendshipState.Active"
      :class="$style.button"
      small
      primary
      @click="deleteFriend(userId, currentUserId)"
    >
      {{ $t("profile.components.FriendshipButtons.removeFriend") }}
    </base-button>
    <template v-else-if="friendshipState === FriendshipState.Pending">
      <base-button
        v-if="state.friendship && state.friendship.invitingUserId === currentUserId"
        :class="$style.button"
        small
        primary
        @click="deleteFriend(userId, currentUserId)"
      >
        {{ $t("profile.components.FriendshipButtons.removeInvitation") }}
      </base-button>
      <template v-else>
        <base-button :class="$style.button" small primary @click="acceptFriend(userId, currentUserId)">
          {{ $t("profile.components.FriendshipButtons.accept") }}
        </base-button>
        <base-button :class="$style.button" small primary @click="declineFriend(userId, currentUserId)">
          {{ $t("profile.components.FriendshipButtons.decline") }}
        </base-button>
      </template>
    </template>
    <base-button
      v-else
      :class="$style.button"
      small
      primary
      @click="inviteFriend(userId, currentUserId)"
    >
      {{ $t("profile.components.FriendshipButtons.invite") }}
    </base-button>
  </div>
</template>