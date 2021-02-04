<script src="./Lobby.ts" lang="ts"></script>
<style src="./Lobby.scss" lang="scss" module></style>

<template>
  <div :class="$style.container">
    <div v-if="lobby">
      <div :class="$style.header">
      <h3>{{ $t("lobby.views.Lobby.title") }}</h3>
        <base-button
          v-if="lobby.status === 0"
          @click="onLeaveLobbyClick"
        >
          {{ $t("lobby.views.Lobby.leave") }}
        </base-button>
      </div>
      <table :class="$style.table">
        <tr>
          <th>{{ $t("lobby.views.Lobby.table.id") }}</th>
          <th>{{ $t("lobby.views.Lobby.table.map") }}</th>
          <th>{{ $t("lobby.views.Lobby.table.members") }}</th>
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
                <span v-if="member.isLeader">
                  ({{ $t("lobby.views.Lobbies.leader") }})
                </span>
              </li>
            </ul>
          </td>
        </tr>
      </table>
      <div
        v-if="match"
        :class="$style.section"
      >
        <h3>{{ $t("lobby.views.Lobby.match") }}</h3>
        <div>Id: {{ match.id }}</div>
        <div>Map: {{ match.map }}</div>
        <div>Status: {{ ["Created", "Started", "Canceled", "Finished"][match.status] }}</div>
        <div>Address: {{ match.address }}</div>
      </div>
      <div
        v-else-if="member && member.isLeader"
        :class="$style.section"
      >
        <h3>{{ $t("lobby.views.Lobby.options") }}</h3>
        <form-field-input
          :label="$t('lobby.views.Lobby.optionsMap')"
          v-model="map"
        />
        <base-button @click="onChangeMapClick">
          {{ $t("lobby.views.Lobby.changeMap") }}
        </base-button>
        <base-button
          primary
          @click="onCloseLobbyClick"
        >
          {{ $t("lobby.views.Lobby.start") }}
        </base-button>
      </div>
    </div>
    <div v-else>
      {{ $t("lobby.views.Lobby.empty") }}
    </div>
  </div>
</template>
