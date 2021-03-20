<script src="./MainHeader.ts" lang="ts"></script>
<style src="./MainHeader.scss" lang="scss" module></style>

<template>
  <header :class="$style.header">
    <div>
      <router-link
        :class="$style.brand"
        :to="{ name: 'home' }"
      >
        Aimrank
      </router-link>
    </div>
    <div v-if="currentUser">
      {{ $t("common.components.MainHeader.user", [currentUser.username]) }}
      <base-button :class="$style.button" small @click="open">Search</base-button>
      <base-button :class="$style.button" small tag="router-link" :to="{ name: 'lobbyInvitations' }">Invitations</base-button>
      <base-button :class="$style.button" small tag="router-link" :to="{ name: 'lobby' }">Lobby</base-button>
      <base-button :class="$style.button" small tag="router-link" :to="{ name: 'profile' }">Profile</base-button>
      <base-button :class="$style.button" small tag="router-link" :to="{ name: 'settings' }">Settings</base-button>
      <base-button :class="$style.button" small primary @click="signOut">Sign out</base-button>
    </div>
  </header>
</template>