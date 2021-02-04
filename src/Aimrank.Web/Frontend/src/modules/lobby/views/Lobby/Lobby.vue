<script src="./Lobby.ts" lang="ts"></script>
<style src="./Lobby.scss" lang="scss" module></style>

<template>
  <div :class="$style.container">
    <div v-if="lobby">
      <div :class="$style.header">
        <h3>Current lobby</h3>
        <base-button
          v-if="lobby.status === 0"
          @click="onLeaveLobbyClick"
        >
          Leave lobby
        </base-button>
      </div>
      <table :class="$style.table">
        <tr>
          <th>Id</th>
          <th>Map</th>
          <th>Members</th>
        </tr>
        <tr>
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
        </tr>
      </table>
      <div
        v-if="match"
        :class="$style.section"
      >
        <h3>Match</h3>
        <div>Id: {{ match.id }}</div>
        <div>Map: {{ match.map }}</div>
        <div>Status: {{ ["Created", "Started", "Canceled", "Finished"][match.status] }}</div>
        <div>Address: {{ match.address }}</div>
      </div>
      <div
        v-else-if="member && member.isLeader"
        :class="$style.section"
      >
        <h3>Options</h3>
        <form-field-input
          label="Map"
          v-model="map"
        />
        <base-button @click="onChangeMapClick">Change map</base-button>
        <base-button
          primary
          @click="onCloseLobbyClick"
        >
          Start game
        </base-button>
      </div>
    </div>
    <div v-else>
      You're not member of any lobby
    </div>
  </div>
</template>
