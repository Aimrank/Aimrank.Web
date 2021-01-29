<script src="./FormFieldInput.ts" lang="ts"></script>
<style src="./FormFieldInput.scss" lang="scss" module></style>

<template>
  <div :class="$style.container">
    <label
      :class="$style.label"
      :for="inputId"
    >
      {{ label }}
    </label>
    <input
      v-bind="$attrs"
      :class="{
        [$style.input]: true,
        [$style.inputError]: errors.length
      }"
      :id="inputId"
      :value="modelValue"
      @input="onInput"
    />
    <form-field-errors
      :class="$style.errors"
      :errors="errors"
      :show-multiple="errorsShowMultiple"
    />
  </div>
</template>
