<script src="./Lobby.ts" lang="ts"></script>
<style src="./Lobby.scss" lang="scss" module></style>

<template>
  <div :class="$style.container">
    <div :class="$style.header">
      <h3>{{ $t("lobby.views.Lobby.title") }}</h3>
      <base-button
        v-if="lobby && lobby.status === 0"
        @click="onLeaveLobbyClick"
      >
        {{ $t("lobby.views.Lobby.leave") }}
      </base-button>
      <base-button
        v-else-if="!lobby"
        primary
        @click="onCreateLobbyClick"
      >
        {{ $t("lobby.views.Lobby.create") }}
      </base-button>
    </div>
    <div v-if="!lobby">{{ $t("lobby.views.Lobby.empty") }}</div>
    <div v-else>
      <table :class="$style.table">
        <tr>
          <th>{{ $t("lobby.views.Lobby.table.map") }}</th>
          <td>{{ lobby.configuration.map }}</td>
        </tr>
        <tr>
          <th>{{ $t("lobby.views.Lobby.table.members") }}</th>
          <td>
            <ul>
              <li
                v-for="member in lobby.members"
                :key="member.userId"
              >
                {{ member.userId }}
                <span v-if="member.isLeader">
                  ({{ $t("lobby.views.Lobby.leader") }})
                </span>
              </li>
            </ul>
          </td>
        </tr>
      </table>
      <div :class="$style.section">
        <h3>{{ $t("lobby.views.Lobby.invitations") }}</h3>
        <invitation-form :lobby-id="lobby.id" />
      </div>
      <div
        v-if="match"
        :class="$style.section"
      >
        <h3>{{ $t("lobby.views.Lobby.match") }}</h3>
        <div>Id: {{ match.id }}</div>
        <div>Map: {{ match.map }}</div>
        <div>Status: {{ ["Created", "Starting", "Started", "Canceled", "Finished"][match.status] }}</div>
        <div>Address: {{ match.address }}</div>
      </div>
      <div
        v-else-if="currentUserMembership && currentUserMembership.isLeader"
        :class="$style.section"
      >
        <h3>{{ $t("lobby.views.Lobby.options") }}</h3>
        <form-field-input
          :label="$t('lobby.views.Lobby.optionsMap')"
          v-model="map"
        />
        <base-button @click="onChangeConfigurationClick">
          {{ $t("lobby.views.Lobby.changeConfiguration") }}
        </base-button>
        <base-button
          primary
          @click="onStartSearchingClick"
        >
          {{ $t("lobby.views.Lobby.start") }}
        </base-button>
      </div>
    </div>
  </div>
</template>
