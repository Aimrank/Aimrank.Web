<script src="./LobbyConfigurationDialog.ts" lang="ts"></script>
<style src="./LobbyConfigurationDialog.scss" lang="scss" module></style>

<template>
  <base-dialog
    :visible="state.isVisible"
    hide-close-icon
  >
    <template #header>
      {{ $t("lobby.components.LobbyConfigurationDialog.title") }}
    </template>
    <div>
      <div :class="$style.section">
        <h4 :class="$style.headline">
          {{ $t("lobby.components.LobbyConfigurationDialog.selectMapHeadline") }}
        </h4>
        <label :class="$style.input">
          <input type="checkbox" v-model="state.maps" value="aim_map" />
          Aim Map
        </label>
        <label :class="$style.input">
          <input type="checkbox" v-model="state.maps" value="am_redline_14" />
          Aim Redline
        </label>
      </div>
      <div :class="$style.section">
        <h4 :class="$style.headline">
          {{ $t("lobby.components.LobbyConfigurationDialog.selectModeHeadline") }}
        </h4>
        <label :class="$style.input">
          <input type="radio" v-model="state.mode" :value="MatchMode.OneVsOne"/>
          {{ $t("lobby.components.LobbyConfigurationDialog.mode.OneVsOne") }}
        </label>
        <label :class="$style.input">
          <input type="radio" v-model="state.mode" :value="MatchMode.TwoVsTwo" />
          {{ $t("lobby.components.LobbyConfigurationDialog.mode.TwoVsTwo") }}
        </label>
      </div>
    </div>
    <template #footer>
      <div :class="$style.footer">
        <base-button
          :class="$style.button"
          :disabled="state.isLoading"
          @click="onCancel"
        >
          {{ $t("lobby.components.LobbyConfigurationDialog.cancel") }}
        </base-button>
        <base-button
          :class="$style.button"
          :disabled="state.isLoading || state.maps.length === 0"
          :loading="state.isLoading"
          primary
          @click="onSubmit"
        >
          {{ $t("lobby.components.LobbyConfigurationDialog.submit") }}
        </base-button>
      </div>
    </template>
  </base-dialog>
</template>