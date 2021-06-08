<script src="./ResetPassword.ts" lang="ts"></script>
<style src="./ResetPassword.scss" lang="scss" module></style>

<template>
  <layout>
    <template #headline>
      {{ $t("authentication.views.ResetPassword.title") }}
    </template>
    <template #default>
      <validation-summary :message="errors.errorMessage" />
      <form @submit.prevent="onSubmit">
        <form-field-input
          :label="$t('authentication.views.ResetPassword.newPassword')"
          type="password"
          v-model="state.newPassword"
          :errors="errors.errors['NewPassword']"
        />
        <form-field-input
          :label="$t('authentication.views.ResetPassword.repeatNewPassword')"
          type="password"
          v-model="state.repeatNewPassword"
          :errors="errors.errors['RepeatNewPassword']"
        />
        <base-button
          :class="$style.button"
          :disabled="!(state.newPassword && state.repeatNewPassword)"
          :loading="loading"
          full-width
        >
          {{ $t("authentication.views.ResetPassword.submit") }}
        </base-button>
      </form>
    </template>
  </layout>
</template>