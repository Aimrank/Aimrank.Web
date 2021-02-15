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
          <th>Mode</th>
          <td>{{ ["One vs One", "Two vs Two"][lobby.configuration.mode] }}</td>
        </tr>
        <tr>
          <th>{{ $t("lobby.views.Lobby.table.map") }}</th>
          <td>
            <img
              :src="maps[lobby.configuration.map]"
              :alt="lobby.configuration.map"
            />
          </td>
        </tr>
        <tr>
          <th>{{ $t("lobby.views.Lobby.table.members") }}</th>
          <td>
            <ul>
              <li
                v-for="member in lobby.members"
                :key="member.userId"
              >
                {{ member.username }}
                <strong v-if="member.isLeader">
                  ({{ $t("lobby.views.Lobby.leader") }})
                </strong>
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
        <div>Map: {{ match.map }}</div>
        <div>Status: {{ ["Created", "Ready", "Canceled", "Starting", "Started", "Finished"][match.status] }}</div>
        <template v-if="match.status === MatchStatus.Started">
          <div>
            Address: aimrank.pl{{ match.address.slice(match.address.indexOf(":")) }}
          </div>
          <code :class="$style.code">
            <pre>
              sv_allowupload 1;
              sv_allowdownload 1;
              connect aimrank.pl{{ match.address.slice(match.address.indexOf(":")) }}
            </pre>
          </code>
        </template>
      </div>
      <div
        v-else-if="currentUserMembership && currentUserMembership.isLeader"
        :class="$style.section"
      >
        <lobby-configuration />
        <base-button
          :class="$style.startButton"
          primary
          @click="onStartSearchingClick"
        >
          {{ $t("lobby.views.Lobby.start") }}
        </base-button>
      </div>
    </div>
  </div>
</template>
