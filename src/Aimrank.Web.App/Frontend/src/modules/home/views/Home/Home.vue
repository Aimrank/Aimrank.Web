<script src="./Home.ts" lang="ts"></script>
<style src="./Home.scss" lang="scss" module></style>

<template>
  <div :class="$style.container">
    <h1 :class="$style.headline">{{ $t("home.views.Home.title") }}</h1>
    <div :class="$style.content">
      <base-button
        :class="$style.button"
        tag="router-link"
        :to="{ name: 'sign-in' }"
        full-width
      >
        {{ $t("home.views.Home.signIn") }}
      </base-button>
      <base-button
        :class="$style.button"
        tag="router-link"
        :to="{ name: 'sign-up' }"
        full-width
      >
        {{ $t("home.views.Home.signUp") }}
      </base-button>
      <base-button
        v-if="isAuthenticated && currentUser"
        :class="$style.button"
        tag="router-link"
        :to="{ name: 'app' }"
        full-width
        primary
      >
        {{ $t("home.views.Home.continue", [currentUser.username]) }}
      </base-button>
    </div>
  </div>
</template>
