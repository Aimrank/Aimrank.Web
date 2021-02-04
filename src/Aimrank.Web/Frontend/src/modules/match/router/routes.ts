import { RouteRecordRaw } from "vue-router";
import { authenticate } from "@/modules/authentication";
import Matches from "../views/Matches";

export const routes: RouteRecordRaw[] = [
  {
    name: "matches",
    path: "/matches",
    component: Matches,
    beforeEnter: authenticate
  }
];
