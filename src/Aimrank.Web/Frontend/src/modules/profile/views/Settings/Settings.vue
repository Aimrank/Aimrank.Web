<script src="./Settings.ts" lang="ts"></script>
<style src="./Settings.scss" lang="scss" module></style>

<template>
  <div :class="$style.container">
    <h3 :class="$style.title">{{ $t("profile.views.Settings.title") }}</h3>
    <div v-if="userDetails">
      <table :class="$style.table">
        <tr>
          <th>{{ $t("profile.views.Settings.username") }}</th>
          <td>{{ userDetails.username }}</td>
          <td></td>
        </tr>
        <tr>
          <th>Steam</th>
          <td>
            <icon
              :class="{
                [$style.icon]: true,
                [$style.iconSuccess]: userDetails.steamId
              }"
              :name="userDetails.steamId ? 'check' : 'times'"
            />
            {{ userDetails.steamId || $t("profile.views.Settings.steamNotConnected") }}
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
    </div>
    <div v-else>
      {{ $t("profile.views.Settings.loading") }}
    </div>
  </div>
</template>
