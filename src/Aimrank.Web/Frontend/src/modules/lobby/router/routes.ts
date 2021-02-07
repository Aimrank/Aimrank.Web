import { RouteRecordRaw } from "vue-router";
import { authenticate } from "@/modules/authentication";
import Invitations from "../views/Invitations";
import Lobby from "../views/Lobby";

export const routes: RouteRecordRaw[] = [
  {
    name: "lobbyInvitations",
    path: "/lobbies/invitations",
    component: Invitations,
    beforeEnter: authenticate
  },
  {
    name: "lobby",
    path: "/lobbies/current",
    component: Lobby,
    beforeEnter: authenticate
  }
];
