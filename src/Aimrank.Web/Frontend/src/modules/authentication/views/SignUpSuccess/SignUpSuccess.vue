<style src="./SignUpSuccess.scss" lang="scss" module></style>

<template>
  <div :class="$style.container">
    <h1 :class="$style.headline">{{ $t("authentication.views.SignUpSuccess.title") }}</h1>
    <p>{{ $t("authentication.views.SignUpSuccess.description") }}</p>
  </div>
</template>