<template>
  <div>
    <router-view />
    <div id="dialogs" />
  </div>
</template>

<style lang="scss">
@use "~/styles/variables/fonts" as fonts;

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
