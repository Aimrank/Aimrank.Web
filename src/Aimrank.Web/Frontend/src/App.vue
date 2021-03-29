<template>
  <div>
    <notifications-list />
    <router-view />
    <div id="dialogs" />
  </div>
</template>

<script lang="ts">
import { defineComponent, onMounted } from "vue";
import { useInitialState } from "@/common/hooks/useInitialState";
import { useNotifications } from "@/common/hooks/useNotifications";
import NotificationsList from "@/common/components/NotificationsList";

const App = defineComponent({
  components: {
    NotificationsList
  },
  setup() {
    const initialState = useInitialState();
    const notifications = useNotifications();

    onMounted(() => {
      const error = initialState.getError();

      if (error) {
        notifications.danger(error);
      }
    });
  }
});

export default App;
</script>
<style lang="scss">
@use "~/styles/variables/fonts" as fonts;

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
