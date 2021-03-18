<script src="./Friends.ts" lang="ts"></script>
<style src="./Friends.scss" lang="scss" module></style>

<template>
  <div :class="$style.container">
    <div
      v-if="isCurrentUserProfile && state.invites.length"
      :class="$style.section"
    >
      <h3 :class="$style.title">{{ $t("profile.views.Friends.invitations") }}</h3>
      <table :class="$style.table">
        <tr>
          <th>Name</th>
          <th></th>
        </tr>
        <tr
          v-for="user in state.invites"
          :key="user.id"
        >
          <td>
            <router-link :to="{ name: 'profile', params: { userId: user.id }}">
              {{ user.username }}
            </router-link>
          </td>
          <td>
            <base-button small @click="onAccept(user.id)">Accept</base-button>
            <base-button small @click="onDecline(user.id)">Decline</base-button>
          </td>
        </tr>
      </table>
    </div>
    <div
      v-if="isCurrentUserProfile && state.blocked.length"
      :class="$style.section"
    >
      <h3 :class="$style.title">{{ $t("profile.views.Friends.blocked") }}</h3>
      <table :class="$style.table">
        <tr>
          <th>Name</th>
          <th></th>
        </tr>
        <tr
          v-for="user in state.blocked"
          :key="user.id"
        >
          <td>
            <router-link :to="{ name: 'profile', params: { userId: user.id }}">
              {{ user.username }}
            </router-link>
          </td>
          <td>
            <base-button small @click="onUnblock(user.id)">Unblock</base-button>
          </td>
        </tr>
      </table>
    </div>
    <div :class="$style.section">
      <h3 :class="$style.title">{{ $t("profile.views.Friends.friends") }}</h3>
      <table :class="$style.table">
        <tr>
          <th>Name</th>
          <th></th>
        </tr>
        <tr
          v-for="user in state.friends"
          :key="user.id"
        >
          <td>
            <router-link :to="{ name: 'profile', params: { userId: user.id }}">
              {{ user.username }}
            </router-link>
          </td>
          <td>
            <base-button
              v-if="isCurrentUserProfile"
              small
              @click="onBlock(user.id)"
            >
              Block
            </base-button>
            <base-button
              v-if="isCurrentUserProfile"
              small
              @click="onDelete(user.id)"
            >
              Delete
            </base-button>
          </td>
        </tr>
      </table>
    </div>
  </div>
</template>