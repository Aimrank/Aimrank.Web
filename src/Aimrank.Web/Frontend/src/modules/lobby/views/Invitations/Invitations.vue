<script src="./Invitations.ts" lang="ts"></script>
<style src="./Invitations.scss" lang="scss" module></style>

<template>
  <div :class="$style.container">
    <div :class="$style.header">
      <h3>{{ $t("lobby.views.Invitations.title") }}</h3>
    </div>
    <table :class="$style.table">
      <tr>
        <th>{{ $t("lobby.views.Invitations.table.from") }}</th>
        <th>{{ $t("lobby.views.Invitations.table.date") }}</th>
        <th></th>
      </tr>
      <tr
        v-for="invitation in invitations"
        :key="invitation.lobbyId"
      >
        <td>{{ invitation.invitingUserName }}</td>
        <td>
          {{ new Date(invitation.createdAt).toLocaleDateString() }}
        </td>
        <td>
          <base-button
            small
            @click="onInvitationCancel(invitation.lobbyId)"
          >
            {{ $t("lobby.views.Invitations.cancel") }}
          </base-button>
          <base-button
            small
            primary
            @click="onInvitationAccept(invitation.lobbyId)"
          >
            {{ $t("lobby.views.Invitations.accept") }}
          </base-button>
        </td>
      </tr>
    </table>
  </div>
</template>