import { RouteRecordRaw } from "vue-router";
import Settings from "@/user/views/Settings";

export const routes: RouteRecordRaw[] = [
  {
    name: "settings",
    path: "settings",
    component: Settings
  }
];
