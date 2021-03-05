<script src="./MatchDialog.ts" lang="ts"></script>
<style src="./MatchDialog.scss" lang="scss" module></style>

<template>
  <base-dialog :visible="isDialogVisible" hide-close-icon>
    <template #header>
      {{ $t("lobby.components.MatchDialog.header") }}
    </template>
    <div v-html="$t('lobby.components.MatchDialog.content', [time])" />
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
          {{ $t("lobby.components.MatchDialog.submit") }}
        </base-button>
      </div>
    </template>
  </base-dialog>
</template>