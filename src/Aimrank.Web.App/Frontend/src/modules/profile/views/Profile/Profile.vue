<script src="./Profile.ts" lang="ts"></script>
<style src="./Profile.scss" lang="scss" module></style>

<template>
  <div
    v-if="state && state.user"
    :class="$style.container"
  >
    <h3 :class="$style.title">{{ $t("profile.views.Profile.title", [state.user.username]) }}</h3>
    <div
      v-if="!isCurrentUserProfile"
      :class="$style.buttons"
    >
      <friendship-buttons />
    </div>
    <div :class="$style.buttons">
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
