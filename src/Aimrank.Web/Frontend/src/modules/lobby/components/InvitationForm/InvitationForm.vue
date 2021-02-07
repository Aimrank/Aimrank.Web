<script src="./InvitationForm.ts" lang="ts"></script>
<style src="./InvitationForm.scss" lang="scss" module></style>

<template>
  <div :class="$style.container">
    <form-field-input
      :label="$t('lobby.components.InvitationForm.label')"
      v-model="username"
    />
    <ul
      v-if="users.length"
      :class="$style.list"
    >
      <li
        v-for="user in users"
        :key="user.userId"
        :class="$style.listItem"
      >
        {{ user.username }}
        <icon
          :class="$style.listItemIcon"
          name="envelope"
          @click="onInviteClick(user.userId)"
        />
      </li>
    </ul>
  </div>
</template>
