<script src="./ResetPassword.ts" lang="ts"></script>
<style src="./ResetPassword.scss" lang="scss" module></style>

<template>
  <div :class="$style.container">
    <h1>{{ $t("authentication.views.ResetPassword.title") }}</h1>
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
        :disabled="!(state.newPassword && state.repeatNewPassword)"
        :loading="loading"
      >
        {{ $t("authentication.views.ResetPassword.submit") }}
      </base-button>
    </form>
  </div>
</template>