<script src="./SignUp.ts" lang="ts"></script>
<style src="./SignUp.scss" lang="scss" module></style>

<template>
  <layout>
    <template #headline>
      {{ $t("authentication.views.SignUp.title") }}
    </template>
    <template #default>
      <form @submit.prevent="onSubmit">
        <validation-summary :message="errors.errorMessage" />
        <form-field-input
          :label="$t('authentication.views.SignUp.form.username')"
          v-model="state.username"
          :errors="errors.errors['Username']"
        />
        <form-field-input
          :label="$t('authentication.views.SignUp.form.email')"
          type="email"
          v-model="state.email"
          :errors="errors.errors['Email']"
        />
        <form-field-input
          :label="$t('authentication.views.SignUp.form.password')"
          type="password"
          v-model="state.password"
          :errors="errors.errors['Password']"
        />
        <form-field-input
          :label="$t('authentication.views.SignUp.form.passwordRepeat')"
          type="password"
          v-model="state.passwordRepeat"
          :errors="errors.errors['PasswordRepeat']"
        />
        <div :class="$style.buttons">
          <base-button :class="$style.button" full-width>
            {{ $t("authentication.views.SignUp.form.submit") }}
          </base-button>
          <base-button :class="$style.button" tag="router-link" :to="{ name: 'sign-in' }" full-width>
            {{ $t("authentication.views.SignUp.signIn") }}
          </base-button>
        </div>
      </form>
    </template>
  </layout>
</template>
