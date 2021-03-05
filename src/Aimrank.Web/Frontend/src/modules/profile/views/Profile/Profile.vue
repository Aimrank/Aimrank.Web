<script src="./Profile.ts" lang="ts"></script>

<template>
  <div v-if="user">
    <h1>Profile: {{ user.username }}</h1>
    <div>
      <routern-view />
    </div>
  </div>
</template>
