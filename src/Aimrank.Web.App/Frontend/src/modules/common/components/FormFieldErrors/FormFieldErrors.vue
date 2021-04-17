<script src="./FormFieldErrors.ts" lang="ts"></script>
<style src="./FormFieldErrors.scss" lang="scss" module></style>

<template>
  <div
    v-if="errors.length"
    :class="$style.errors"
  >
    <ul
      v-if="showMultiple"
      :class="$style.list"
    >
      <li
        v-for="error in errors"
        :key="error"
        :class="$style.listItem"
      >
        {{ error }}
      </li>
    </ul>
    <span v-else>{{ errors[0] }}</span>
  </div>
</template>
