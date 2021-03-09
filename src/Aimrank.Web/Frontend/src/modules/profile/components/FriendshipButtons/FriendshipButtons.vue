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
      {{ state.friendship.blockingUsersIds.includes(currentUserId) ? "Unblock" : "You are blocked" }}
    </base-button>
    <base-button
      v-else-if="friendshipState === FriendshipState.Active"
      :class="$style.button"
      small
      primary
      @click="deleteFriend(userId, currentUserId)"
    >
      Remove friend
    </base-button>
    <template v-else-if="friendshipState === FriendshipState.Pending">
      <base-button
        v-if="state.friendship && state.friendship.invitingUserId === currentUserId"
        :class="$style.button"
        small
        primary
        @click="deleteFriend(userId, currentUserId)"
      >
        Remove invitation
      </base-button>
      <template v-else>
        <base-button :class="$style.button" small primary @click="acceptFriend(userId, currentUserId)">Accept</base-button>
        <base-button :class="$style.button" small primary @click="declineFriend(userId, currentUserId)">Decline</base-button>
      </template>
    </template>
    <base-button
      v-else
      :class="$style.button"
      small
      primary
      @click="inviteFriend(userId, currentUserId)"
    >
      Send Invitation
    </base-button>
  </div>
</template>