<script src="./Home.ts" lang="ts"></script>
<style src="./Home.scss" lang="scss" module></style>

<template>
  <div :class="$style.container">
    <h1 :class="$style.headline">{{ $t("home.views.Home.title") }}</h1>
    <div>
      <base-button :class="$style.button" tag="router-link" :to="{ name: 'sign-in' }">Sign in</base-button>
    </div>
    <div>
      <base-button :class="$style.button" tag="router-link" :to="{ name: 'sign-up' }">Sign up</base-button>
    </div>
  </div>
</template>
