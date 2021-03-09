<script src="./UsersDialog.ts" lang="ts"></script>
<style src="./UsersDialog.scss" lang="scss" module></style>

<template>
  <base-dialog
    :visible="state.isDialogVisible"
    @click:outside="onClose"
    @close="onClose"
  >
    <template #header>
      {{ $t("common.components.UsersDialog.title") }}
    </template>
    <div>
      <form-field-input
        :placeholder="$t('common.components.UsersDialog.label')"
        v-model="state.searchQuery"
      />
      <ul :class="$style.list">
        <li
          v-for="user in state.searchResults"
          :key="user.id"
          :class="$style.listItem"
          @click="onUserClick(user.id)"
        >
          {{ user.username }}
        </li>
      </ul>
    </div>
  </base-dialog>
</template>