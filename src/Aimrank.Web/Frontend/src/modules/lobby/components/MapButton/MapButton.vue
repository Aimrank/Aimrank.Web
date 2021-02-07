<script src="./MapButton.ts" lang="ts"></script>
<style src="./MapButton.scss" lang="scss" module></style>

<template>
  <button
    :class="$style.button"
    @click="onButtonClick"
  >
    <img
      :class="{
        [$style.image]: true,
        [$style.imageSelected]: selected
      }"
      :src="image"
      alt="map"
    />
  </button>
</template>
