<script src="./MatchDialog.ts" lang="ts"></script>
<style src="./MatchDialog.scss" lang="scss" module></style>

<template>
  <base-dialog :visible="isDialogVisible" hide-close-icon>
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
      <div :class="$style.footer">
        <base-button
          primary
          :loading="isAcceptedByUser && totalAcceptations === totalAcceptationsNeeded"
          :disabled="isAcceptedByUser && totalAcceptations < totalAcceptationsNeeded"
          @click="onAcceptClick"
        >
          {{ $t("match.components.MatchDialog.submit") }}
        </base-button>
      </div>
    </template>
  </base-dialog>
</template>