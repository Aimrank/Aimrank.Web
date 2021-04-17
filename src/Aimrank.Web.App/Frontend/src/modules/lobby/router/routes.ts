import { RouteRecordRaw } from "vue-router";
import Invitations from "@/lobby/views/Invitations";
import Lobby from "@/lobby/views/Lobby";

export const routes: RouteRecordRaw[] = [
  {
    name: "lobbyInvitations",
    path: "lobbies/invitations",
    component: Invitations
  },
  {
    name: "lobby",
    path: "lobbies/current",
    component: Lobby
  }
];
