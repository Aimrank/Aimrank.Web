<style src="./AppHome.scss" lang="scss" module></style>

<template>
  <div :class="$style.container">
    <div
      :class="$style.content"
      v-html="$t('app.views.AppHome.description')"
    />
  </div>
</template>
