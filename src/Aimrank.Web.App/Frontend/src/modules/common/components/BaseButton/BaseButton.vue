<script src="./BaseButton.ts" lang="ts"></script>
<style src="./BaseButton.scss" lang="scss" module></style>

<template>
  <component
    :is="tag"
    :class="{
      [$style.button]: true,
      [$style.buttonSmall]: small,
      [$style.buttonPrimary]: primary,
      [$style.buttonLoading]: loading,
      [$style.buttonDisabled]: disabled
    }"
    :disabled="disabled"
    v-bind="$attrs"
  >
    <template v-if="loading">
      <slot name="loading">
        <span :class="$style.loader" />
      </slot>
    </template>
    <div :class="$style.slot">
      <slot />
    </div>
  </component>
</template>
