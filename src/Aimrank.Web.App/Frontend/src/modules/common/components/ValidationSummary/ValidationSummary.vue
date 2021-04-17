<script src="./ValidationSummary.ts" lang="ts"></script>
<style src="./ValidationSummary.scss" lang="scss" module></style>

<template>
  <div
    v-if="message"
    :class="$style.message"
  >
    {{ message }}
  </div>
</template>
