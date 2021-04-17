<script src="./LobbyInvitationDialog.ts" lang="ts"></script>
<style src="./LobbyInvitationDialog.scss" lang="scss" module></style>

<template>
  <base-dialog
    :visible="isVisible"
    @click:outside="close"
    @close="close"
  >
    <template #header>
      {{ $t("lobby.components.LobbyInvitationDialog.title") }}
    </template>
    <div>
      <ul :class="$style.list">
        <li
          v-for="user in users"
          :key="user.id"
          :class="$style.listItem"
        >
          {{ user.username }}
          <base-button
            :class="$style.button"
            small
            primary
            @click="onInviteClick(user.id)"
          >
            {{ $t("lobby.components.LobbyInvitationDialog.invite") }}
          </base-button>
        </li>
      </ul>
    </div>
  </base-dialog>
</template>