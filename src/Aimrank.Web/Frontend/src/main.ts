import { createApp } from "vue";
import { createI18n } from "vue-i18n";
import { router } from "@/router";
import { httpClient } from "@/services";
import { useAuth, useUser } from "./modules/authentication";

import App from "./App.vue";

const en = require("@/locales/en.json");

const init = () => {
  initAuthentication();

  const app = createApp(App);
  const i18n = createI18n({
    locale: "en",
    messages: { en }
  });

  app.use(i18n);
  app.use(router);
  app.mount("#root");
}

const initAuthentication = () => {
  const auth = useAuth();
  const user = useUser();

  httpClient.loadTokens();

  const userId = httpClient.getUserId();
  const userEmail = httpClient.getUserEmail();

  if (userId && userEmail) {
    auth.setAuthenticated(true);
    user.setUser({
      id: userId,
      email: userEmail
    });
  }
}

init();
