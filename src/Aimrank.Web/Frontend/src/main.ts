import { createApp } from "vue";
import { createI18n } from "vue-i18n";
import { router } from "@/router";
import { httpClient, generalHub } from "@/services";
import { useAuth } from "./modules/authentication";
import { useUser } from "./modules/user";
import { useNotifications } from "./modules/common/hooks/useNotifications";

import App from "./App.vue";

const en = require("@/locales/en.json");

const init = async () => {
  await initAuthentication();

  const app = createApp(App);
  const i18n = createI18n({
    locale: "en",
    messages: { en }
  });

  app.use(i18n);
  app.use(router);
  app.mount("#root");
}

const initAuthentication = async () => {
  const auth = useAuth();
  const user = useUser();
  const notifications = useNotifications();

  httpClient.loadTokens();

  const userId = httpClient.getUserId();
  const userEmail = httpClient.getUserEmail();

  if (userId && userEmail) {
    auth.setAuthenticated(true);
    await user.updateUser(userId);

    generalHub.connection.on("MatchStarting", () => {
      notifications.success("Starting new server...");
    });

    generalHub.connection.on("MatchStarted", (event) => {
      notifications.success(`Match started: ${event.address}`);
    });

    generalHub.connection.on("MatchFinished", (event) => {
      notifications.success(`Match finished: ${event.matchId}`);
    });

    await generalHub.connect();
  }
}

init();
