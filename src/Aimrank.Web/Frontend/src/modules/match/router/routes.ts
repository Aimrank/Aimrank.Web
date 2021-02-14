import { RouteRecordRaw } from "vue-router";
import Matches from "@/match/views/Matches";

export const routes: RouteRecordRaw[] = [
  {
    name: "matches",
    path: "matches",
    component: Matches
  }
];
