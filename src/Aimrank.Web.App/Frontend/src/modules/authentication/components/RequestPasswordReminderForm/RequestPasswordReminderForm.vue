<script src="./RequestPasswordReminderForm.ts" lang="ts"></script>
<style src="./RequestPasswordReminderForm.scss" lang="scss" module></style>

<template>
  <div>
    <validation-summary :message="errors.errorMessage" />
    <form @submit.prevent="onSubmit">
      <form-field-input
        :label="$t('authentication.components.RequestPasswordReminderForm.usernameOrEmail')"
        v-model="state.usernameOrEmail"
        :errors="errors.errors['UsernameOrEmail']"
      />
      <base-button
        :class="$style.button"
        :disabled="!(state.usernameOrEmail)"
        :loading="loading"
        full-width
      >
        {{ $t("authentication.components.RequestPasswordReminderForm.submit") }}
      </base-button>
    </form>
  </div>
</template>
