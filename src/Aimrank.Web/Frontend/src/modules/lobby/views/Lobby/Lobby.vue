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
          <th>{{ $t("lobby.views.Lobby.table.mode") }}</th>
          <td>{{ ["One vs One", "Two vs Two"][lobby.configuration.mode] }}</td>
        </tr>
        <tr>
          <th>{{ $t("lobby.views.Lobby.table.maps") }}</th>
          <td>
            <div :class="$style.tableImages">
              <img
                :class="$style.tableImage"
                v-for="map in lobby.configuration.maps"
                :key="map"
                :alt="map"
                :src="maps[map]"
              />
            </div>
          </td>
        </tr>
        <tr>
          <th>{{ $t("lobby.views.Lobby.table.members") }}</th>
          <td>
            <ul>
              <li
                v-for="member in members"
                :key="member.user.id"
              >
                <router-link :to="{ name: 'profile', params: { userId: member.user.id }}">
                  {{ member.user.username }}
                </router-link>
                <strong v-if="member.isLeader">
                  ({{ $t("lobby.views.Lobby.leader") }})
                </strong>
                <base-button
                  v-else-if="isCurrentUserLeader"
                  small
                  @click="onKickPlayerClick(member.user.id)"
                >
                  {{ $t("lobby.views.Lobby.kick") }}
                </base-button>
              </li>
            </ul>
          </td>
        </tr>
      </table>
      <div :class="$style.section">
        <h3>{{ $t("lobby.views.Lobby.invitations") }}</h3>
        <base-button @click="invitationDialog.open">
          {{ $t("lobby.views.Lobby.inviteButton") }}
        </base-button>
        <lobby-invitation-dialog
          :lobby-id="lobby.id"
          :is-visible="invitationDialog.isVisible.value"
          @close="invitationDialog.close"
        />
      </div>
      <div
        v-if="lobby.match"
        :class="$style.section"
      >
        <h3>{{ $t("lobby.views.Lobby.match") }}</h3>
        <div>Map: {{ lobby.match.map }}</div>
        <div>Status: {{ ["Created", "Ready", "Starting", "Started", "Finished"][lobby.match.status] }}</div>
        <template v-if="lobby.match.status === MatchStatus.Started">
          <div>
            Address: aimrank.pl{{ lobby.match.address.slice(lobby.match.address.indexOf(":")) }}
          </div>
          <code :class="$style.code">
            <pre>
              sv_allowupload 1;
              sv_allowdownload 1;
              connect aimrank.pl{{ lobby.match.address.slice(lobby.match.address.indexOf(":")) }}
            </pre>
          </code>
        </template>
      </div>
      <div
        v-else-if="isCurrentUserLeader && lobby.status === 0"
        :class="$style.section"
      >
        <base-button @click="onChangeMapsClick">
          {{ $t("lobby.views.Lobby.changeConfiguration") }}
        </base-button>
        <base-button
          :class="$style.startButton"
          primary
          @click="onStartSearchingClick"
        >
          {{ $t("lobby.views.Lobby.start") }}
        </base-button>
      </div>
      <lobby-configuration-dialog
        ref="configurationDialog"
        :configuration="{
          lobbyId: lobby.id,
          maps: lobby.configuration.maps,
          mode: lobby.configuration.mode,
          name: lobby.configuration.name
        }"
      />
    </div>
  </div>
</template>
