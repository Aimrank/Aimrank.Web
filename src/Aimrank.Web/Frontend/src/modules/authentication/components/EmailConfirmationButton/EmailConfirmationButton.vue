<script src="./EmailConfirmationButton.ts" lang="ts"></script>

<template>
  <base-button
    :loading="loading"
    @click="onResendEmailConfirmation"
  >
    {{ $t("authentication.components.EmailConfirmationButton.text") }}
  </base-button>
</template>