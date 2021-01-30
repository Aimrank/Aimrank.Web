import { createApp } from "vue";
import { createI18n } from "vue-i18n";
import { router } from "@/router";
import { httpClient } from "@/services";
import { useAuth } from "./modules/authentication";
import { useUser } from "./modules/user";

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

  httpClient.loadTokens();

  const userId = httpClient.getUserId();
  const userEmail = httpClient.getUserEmail();

  if (userId && userEmail) {
    auth.setAuthenticated(true);
    await user.updateUser(userId);
  }
}

init();
