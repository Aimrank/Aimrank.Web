<script src="./ChangePasswordForm.ts" lang="ts"></script>

<template>
  <form @submit.prevent="onChangePasswordSubmit">
    <validation-summary :message="passwordFormErrors.errorMessage" />
    <form-field-input
      :label="$t('profile.components.ChangePasswordForm.oldPassword')"
      v-model="passwordForm.oldPassword"
      :errors="passwordFormErrors.errors['OldPassword']"
      type="password"
    />
    <form-field-input
      :label="$t('profile.components.ChangePasswordForm.newPassword')"
      v-model="passwordForm.newPassword"
      :errors="passwordFormErrors.errors['NewPassword']"
      type="password"
    />
    <form-field-input
      :label="$t('profile.components.ChangePasswordForm.repeatNewPassword')"
      v-model="passwordForm.repeatNewPassword"
      :errors="passwordFormErrors.errors['RepeatNewPassword']"
      type="password"
    />
    <base-button
      :disabled="!(passwordForm.oldPassword && passwordForm.newPassword && passwordForm.repeatNewPassword)"
      :loading="loading"
    >
      {{ $t("profile.components.ChangePasswordForm.submit") }}
    </base-button>
  </form>
</template>