<template>
  <div>
    <main-header />
    <router-view />
  </div>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import MainHeader from "@/modules/common/components/MainHeader";

export default defineComponent({
  components: {
    MainHeader
  }
});
</script>

<style lang="scss">
@use "@/styles/variables/fonts" as fonts;

* {
  margin: 0;
  padding: 0;
  box-sizing: border-box;
}

html {
  font-size: percentage(10 / 16);
}

body {
  font-family: fonts.$family;
  font-size: fonts.$sizeBase;
}
</style>
