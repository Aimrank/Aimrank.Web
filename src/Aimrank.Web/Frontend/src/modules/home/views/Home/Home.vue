<style src="./Home.scss" lang="scss" module></style>

<template>
  <div :class="$style.container">
    <div :class="$style.column">
      <h3>{{ $t("home.views.Home.board") }}</h3>
    </div>
    <div :class="$style.column">
      <h3>{{ $t("home.views.Home.history") }}</h3>
    </div>
  </div>
</template>
