<script src="./SignUpSuccess.ts" lang="ts"></script>
<style src="./SignUpSuccess.scss" lang="scss" module></style>

<template>
  <div :class="$style.container">
    <h1 :class="$style.headline">{{ $t("authentication.views.SignUpSuccess.title") }}</h1>
    <p :class="$style.text">{{ $t("authentication.views.SignUpSuccess.description") }}</p>
    <p :class="$style.text">{{ $t("authentication.views.SignUpSuccess.resendEmailTitle") }}</p>
    <email-confirmation-button :username-or-email="route.query.email" />
  </div>
</template>