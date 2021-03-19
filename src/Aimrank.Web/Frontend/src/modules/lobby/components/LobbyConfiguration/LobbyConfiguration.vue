<script src="./LobbyConfiguration.ts" lang="ts"></script>
<style src="./LobbyConfiguration.scss" lang="scss" module></style>

<template>
  <div>
    <h3>{{ $t("lobby.components.LobbyConfiguration.selectMapHeadline") }}</h3>
    <div>
      <map-button
        name="aim_map"
        :image="maps['aim_map']"
        :selected="map === 'aim_map'"
        @click="onMapSelected('aim_map')"
      />
      <map-button
        name="am_redline_14"
        :image="maps['am_redline_14']"
        :selected="map === 'am_redline_14'"
        @click="onMapSelected('am_redline_14')"
      />
    </div>
    <h3>{{ $t("lobby.components.LobbyConfiguration.selectModeHeadline") }}</h3>
    <div>
      <button
        :class="{
          [$style.button]: true,
          [$style.buttonSelected]: mode === MatchMode.OneVsOne
        }"
        @click="onModeSelected(MatchMode.OneVsOne)"
      >
        {{ $t("lobby.components.LobbyConfiguration.mode.OneVsOne") }}
      </button>
      <button
        :class="{
          [$style.button]: true,
          [$style.buttonSelected]: mode === MatchMode.TwoVsTwo
        }"
        @click="onModeSelected(MatchMode.TwoVsTwo)"
      >
        {{ $t("lobby.components.LobbyConfiguration.mode.TwoVsTwo") }}
      </button>
    </div>
  </div>
</template>