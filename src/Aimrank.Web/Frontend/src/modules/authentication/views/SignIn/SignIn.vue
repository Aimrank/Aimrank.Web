<script src="./SignIn.ts" lang="ts"></script>
<style src="./SignIn.scss" lang="scss" module></style>

<template>
  <div :class="$style.container">
    <h1>{{ $t("authentication.views.SignIn.title") }}</h1>
    <validation-summary :message="errors.errorMessage" />
    <form @submit.prevent="onSubmit">
      <form-field-input
        :label="$t('authentication.views.SignIn.usernameOrEmail')"
        v-model="state.usernameOrEmail"
        :errors="errors.errors['UsernameOrEmail']"
      />
      <form-field-input
        :label="$t('authentication.views.SignIn.password')"
        type="password"
        v-model="state.password"
        :errors="errors.errors['Password']"
      />
      <base-button :disabled="!(state.usernameOrEmail && state.password)">
        {{ $t("authentication.views.SignIn.submit") }}
      </base-button>
    </form>
    <div
      v-if="state.emailNotConfirmed"
      :class="$style.section"
    >
      <p :class="$style.sectionText">{{ $t("authentication.views.SignIn.emailNotConfirmed") }}</p>
      <email-confirmation-button :username-or-email="state.usernameOrEmail" />
    </div>
    <div :class="$style.section">
      <p :class="$style.sectionText">{{ $t("authentication.views.SignIn.requestPasswordReminder") }}</p>
      <request-password-reminder-form />
    </div>
  </div>
</template>
