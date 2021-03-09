<script src="./Profile.ts" lang="ts"></script>
<style src="./Profile.scss" lang="scss" module></style>

<template>
  <div
    v-if="state.user"
    :class="$style.container"
  >
    <h3 :class="$style.title">{{ $t("profile.views.Profile.title", [state.user.username]) }}</h3>
    <div :class="$style.buttons">
      <base-button
        v-if="friendship && friendshipState === FriendshipState.Blocked"
        :disabled="!friendship.blockingUsersIds.includes(user.state.user.id)"
        small
        @click="onUnblock"
      >
        {{ friendship?.blockingUsersIds.includes(user.state.user.id) ? "Unblock" : "You are blocked" }}
      </base-button>
      <base-button
        v-else-if="friendshipState === FriendshipState.Active"
        small
      >
        Remove friend
      </base-button>
      <template v-else-if="friendshipState === FriendshipState.Pending">
        <base-button
          v-if="friendship && friendship.invitingUserId === user.state.user.id"
          @click="onDelete"
        >
          Remove invitation
        </base-button>
        <template v-else>
          <base-button small @click="onAccept">Accept</base-button>
          <base-button small @click="onDecline">Decline</base-button>
        </template>
      </template>
      <base-button
        v-else-if="!isSelf"
        small
        @click="onInvite"
      >
        Send Invitation
      </base-button>
      <base-button
        tag="router-link"
        v-for="link in links"
        :key="link.name"
        small
        :to="{
          name: link.name,
          params: link.params
        }"
      >
        {{ link.label }}
      </base-button>
    </div>
    <div>
      <router-view />
    </div>
  </div>
</template>
