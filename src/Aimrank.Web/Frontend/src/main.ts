import { createApp } from "vue";
import { createI18n } from "vue-i18n";
import { router } from "@/router";

import App from "./App.vue";

const en = require("@/locales/en.json");

const init = () => {
  const app = createApp(App);
  const i18n = createI18n({
    locale: "en",
    messages: { en }
  });

  app.use(i18n);
  app.use(router);
  app.mount("#root");
}

init();
