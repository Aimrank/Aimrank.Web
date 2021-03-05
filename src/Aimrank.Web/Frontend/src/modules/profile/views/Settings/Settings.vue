<script src="./Settings.ts" lang="ts"></script>
<style src="./Settings.scss" lang="scss" module></style>

<template>
  <div :class="$style.container">
    <h3 :class="$style.title">{{ $t("user.views.Settings.title") }}</h3>
    <div v-if="userDetails">
      <table :class="$style.table">
        <tr>
          <th>Name</th>
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
            {{ userDetails.steamId || $t("user.views.Settings.steamNotConnected") }}
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
      {{ $t("user.views.Settings.loading") }}
    </div>
  </div>
</template>
