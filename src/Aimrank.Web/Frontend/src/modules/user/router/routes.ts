import { RouteRecordRaw } from "vue-router";
import Settings from "../views/Settings";

export const routes: RouteRecordRaw[] = [
  {
    name: "settings",
    path: "settings",
    component: Settings
  }
];
