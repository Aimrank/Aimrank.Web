<script src="./Lobbies.ts" lang="ts"></script>
<style src="./Lobbies.scss" lang="scss" module></style>

<template>
  <div :class="$style.container">
    <div :class="$style.header">
      <h3>{{ $t("lobby.views.Lobbies.title") }}</h3>
      <base-button
        primary
        @click="onCreateNewLobbyClick"
      >
        {{ $t("lobby.views.Lobbies.create") }}
      </base-button>
    </div>
    <table :class="$style.table">
      <tr>
        <th>{{ $t("lobby.views.Lobbies.table.id") }}</th>
        <th>{{ $t("lobby.views.Lobbies.table.map") }}</th>
        <th>{{ $t("lobby.views.Lobbies.table.members") }}</th>
        <th></th>
      </tr>
      <tr
        v-for="lobby in lobbies"
        :key="lobby.id"
      >
        <td>{{ lobby.id }}</td>
        <td>{{ lobby.map }}</td>
        <td>
          <ul>
            <li
              v-for="member in lobby.members"
              :key="member.userId"
            >
              {{ member.userId }}
              <span v-if="member.isLeader">({{ $t("lobby.views.Lobbies.leader") }})</span>
            </li>
          </ul>
        </td>
        <td>
          <base-button
            v-if="lobby.members.every(m => userState.user && m.userId !== userState.user.id)"
            @click="onJoinClick(lobby.id)"
          >
            {{ $t("lobby.views.Lobbies.join") }}
          </base-button>
        </td>
      </tr>
    </table>
  </div>
</template>
