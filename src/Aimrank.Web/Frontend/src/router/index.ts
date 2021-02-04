import { createRouter, createWebHistory } from "vue-router";

import { routes as home } from "@/modules/home";
import { routes as user } from "@/modules/user";
import { routes as authentication } from "@/modules/authentication";
import { routes as lobby } from "@/modules/lobby";
import { routes as match } from "@/modules/match";

export const router = createRouter({
  history: createWebHistory(),
  routes: [
    ...home,
    ...user,
    ...authentication,
    ...lobby,
    ...match
  ]
});
