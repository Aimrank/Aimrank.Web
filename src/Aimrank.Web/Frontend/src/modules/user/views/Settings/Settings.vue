<script src="./Settings.ts" lang="ts"></script>
<style src="./Settings.scss" lang="scss" module></style>

<template>
  <div :class="$style.container">
    <h1>{{ $t("user.views.Settings.title") }}</h1>
    <div v-if="userDetails">
      <div>UserId: {{ userDetails.userId }}</div>
      <div>
        SteamId: {{ userDetails.steamId || $t("user.views.Settings.steamNotConnected") }}
        <button @click="onConnectSteamAccount">
          {{ $t(`user.views.Settings.connectSteamButton${userDetails.steamId ? "" : "Not"}Connected`) }}
        </button>
      </div>
      <div>Username: {{ userDetails.username }}</div>
    </div>
    <div v-else>
      {{ $t("user.views.Settings.loading") }}
    </div>
  </div>
</template>
