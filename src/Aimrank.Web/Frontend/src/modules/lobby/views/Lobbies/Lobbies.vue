<script src="./Lobbies.ts" lang="ts"></script>
<style src="./Lobbies.scss" lang="scss" module></style>

<template>
  <div :class="$style.container">
    <div :class="$style.header">
      <h3>Opened lobbies</h3>
      <base-button
        primary
        @click="onCreateNewLobbyClick"
      >
        Create new lobby
      </base-button>
    </div>
    <table :class="$style.table">
      <tr>
        <th>Id</th>
        <th>Map</th>
        <th>Members</th>
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
              <span v-if="member.isLeader">(Leader)</span>
            </li>
          </ul>
        </td>
        <td>
          <base-button
            v-if="lobby.members.every(m => userState.user && m.userId !== userState.user.id)"
            @click="onJoinClick(lobby.id)"
          >
            Join
          </base-button>
        </td>
      </tr>
    </table>
  </div>
</template>
