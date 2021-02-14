<template>
  <div>
    <main-header />
    <match-dialog />
    <notifications-list />
    <router-view />
    <div id="dialogs" />
  </div>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import MainHeader from "@/modules/common/components/MainHeader";
import MatchDialog from "@/modules/match/components/MatchDialog";
import NotificationsList from "@/modules/common/components/NotificationsList";

export default defineComponent({
  components: {
    MainHeader,
    MatchDialog,
    NotificationsList
  }
});
</script>

<style lang="scss">
@use "@/styles/variables/fonts" as fonts;

* {
  margin: 0;
  padding: 0;
  box-sizing: border-box;
}

html {
  font-size: percentage(10 / 16);
}

body {
  font-family: fonts.$family;
  font-size: fonts.$sizeBase;
}
</style>
