<script src="./Settings.ts" lang="ts"></script>
<style src="./Settings.scss" lang="scss" module></style>

<template>
  <div :class="$style.container">
    <div v-if="state && state.user">
      <h3 :class="$style.title">{{ $t("profile.views.Settings.title") }}</h3>
      <table :class="$style.table">
        <tr>
          <th>{{ $t("profile.views.Settings.username") }}</th>
          <td>{{ state.user.username }}</td>
          <td></td>
        </tr>
        <tr>
          <th>Steam</th>
          <td>
            <icon
              :class="{
                [$style.icon]: true,
                [$style.iconSuccess]: state.user.steamId
              }"
              :name="state.user.steamId ? 'check' : 'times'"
            />
            {{ state.user.steamId || $t("profile.views.Settings.steamNotConnected") }}
          </td>
          <td>
            <base-button
              :class="$style.button"
              primary
              @click="onConnectSteamAccount"
            >
              <icon name="steam" />
            </base-button>
          </td>
        </tr>
      </table>
      <h3 :class="$style.title">{{ $t("profile.views.Settings.changePasswordForm") }}</h3>
      <change-password-form />
    </div>
    <div v-else>
      {{ $t("profile.views.Settings.loading") }}
    </div>
  </div>
</template>
