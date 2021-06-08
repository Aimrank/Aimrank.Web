<script src="./Layout.ts" lang="ts"></script>
<style src="./Layout.scss" lang="scss" module></style>

<template>
  <div :class="$style.container">
    <div :class="$style.sidebar">
      <base-button
        :class="$style.button"
        tag="router-link"
        :to="{ name: 'admin:dashboard' }"
        full-width
      >
        {{ $t("admin.views.Layout.sidebar.dashboard") }}
      </base-button>
      <base-button
        :class="$style.button"
        tag="router-link"
        :to="{ name: 'admin:steamTokens' }"
        full-width
      >
        {{ $t("admin.views.Layout.sidebar.steamTokens") }}
      </base-button>
    </div>
    <div :class="$style.content">
      <router-view />
    </div>
  </div>
</template>