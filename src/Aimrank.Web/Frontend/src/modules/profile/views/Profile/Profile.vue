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
        tag="router-link"
        v-for="link in links"
        :key="link.name"
        :class="$style.button"
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
