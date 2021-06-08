<script src="./SignIn.ts" lang="ts"></script>
<style src="./SignIn.scss" lang="scss" module></style>

<template>
  <layout>
    <template #headline>
      {{ $t("authentication.views.SignIn.title") }}
    </template>
    <template #default>
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
        <base-button
          :class="$style.button"
          :disabled="!(state.usernameOrEmail && state.password)"
          full-width
        >
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
      <div :class="$style.section">
        <base-button
          :class="$style.button"
          tag="router-link"
          :to="{ name: 'sign-up' }"
          full-width
        >
          {{ $t("authentication.views.SignIn.signUp") }}
        </base-button>
      </div>
    </template>
  </layout>
</template>
