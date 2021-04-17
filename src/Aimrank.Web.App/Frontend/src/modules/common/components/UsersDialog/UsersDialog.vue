<script src="./UsersDialog.ts" lang="ts"></script>
<style src="./UsersDialog.scss" lang="scss" module></style>

<template>
  <base-dialog
    :visible="isVisible"
    @click:outside="close"
    @close="close"
  >
    <template #header>
      {{ $t("common.components.UsersDialog.title") }}
    </template>
    <div>
      <form-field-input
        :placeholder="$t('common.components.UsersDialog.label')"
        v-model="username"
      />
      <ul :class="$style.list">
        <li
          v-for="user in users"
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