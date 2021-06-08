<script src="./Layout.ts" lang="ts"></script>
<style src="./Layout.scss" lang="scss" module></style>

<template>
  <div :class="$style.container">
    <h1
      v-if="$slots.headline"
      :class="$style.headline"
    >
      <slot name="headline" />
    </h1>
    <div :class="$style.content">
      <slot />
    </div>
  </div>
</template>