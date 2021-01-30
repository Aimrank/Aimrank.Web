import { RouteRecordRaw } from "vue-router";
import { authenticate } from "@/modules/authentication";
import Settings from "../views/Settings";

export const routes: RouteRecordRaw[] = [
  {
    name: "settings",
    path: "/settings",
    component: Settings,
    beforeEnter: authenticate
  }
];
