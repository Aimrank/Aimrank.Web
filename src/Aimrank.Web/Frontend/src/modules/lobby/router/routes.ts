import { RouteRecordRaw } from "vue-router";
import { authenticate } from "@/modules/authentication";
import Lobbies from "../views/Lobbies";
import Lobby from "../views/Lobby";

export const routes: RouteRecordRaw[] = [
  {
    name: "lobbies",
    path: "/lobbies",
    component: Lobbies,
    beforeEnter: authenticate
  },
  {
    name: "lobby",
    path: "/lobbies/current",
    component: Lobby,
    beforeEnter: authenticate
  }
];
