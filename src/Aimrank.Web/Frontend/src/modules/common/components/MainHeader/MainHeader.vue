<script src="./MainHeader.ts" lang="ts"></script>
<style src="./MainHeader.scss" lang="scss" module></style>

<template>
  <header :class="$style.header">
    <div>
      <router-link :to="{ name: 'home' }">Home</router-link>
    </div>
    <div v-if="authState.isAuthenticated">
      Hello {{ userState.user.username }}
      <button @click="signOut">Sign out</button>
    </div>
    <div v-else>
      <router-link :to="{ name: 'sign-in' }">Sign in</router-link>
      <router-link :to="{ name: 'sign-up' }">Sign up</router-link>
    </div>
  </header>
</template>