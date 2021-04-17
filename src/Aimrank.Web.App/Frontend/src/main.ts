import { createApp } from "vue";
import { createI18n } from "vue-i18n";
import { router } from "~/router";
import { useAuth } from "@/authentication/hooks/useAuth";
import { useInitialState } from "@/common/hooks/useInitialState";

import App from "./App.vue";

const en = require("~/locales/en.json");

const i18n = createI18n({
  locale: "en",
  messages: { en }
});

const init = () => {
  const { setCurrentUser } = useAuth();
  const { getUser } = useInitialState();
  setCurrentUser(getUser());

  createApp(App)
    .use(i18n)
    .use(router)
    .mount("#root");
}

init();
