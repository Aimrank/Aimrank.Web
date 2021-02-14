<script src="./MatchDialog.ts" lang="ts"></script>
<style src="./MatchDialog.scss" lang="scss" module></style>

<template>
  <base-dialog :visible="isDialogVisible">
    <template #header>
      {{ $t("match.components.MatchDialog.header") }}
    </template>
    <div v-html="$t('match.components.MatchDialog.content', [time])" />
    <div :class="$style.icons">
      <div
        v-for="index in totalAcceptationsNeeded"
        :key="index"
        :class="{
          [$style.iconBox]: true,
          [$style.iconBoxSelected]: index <= totalAcceptations
        }"
      >
        <icon name="check" />
      </div>
    </div>
    <template #footer>
      <base-button
        primary
        @click="onAcceptClick"
      >
        {{ $t("match.components.MatchDialog.submit") }}
      </base-button>
    </template>
  </base-dialog>
</template>