import { createRouter, createWebHistory } from "vue-router";

import { authenticate } from "@/authentication/router/authenticationGuard";

import { routes as authentication } from "@/authentication/router/routes";
import { routes as home } from "@/home/router/routes";
import { routes as user } from "@/user/router/routes";
import { routes as lobby } from "@/lobby/router/routes";
import { routes as match } from "@/match/router/routes";

import AppAuthenticated from "~/AppAuthenticated.vue";

export const router = createRouter({
  history: createWebHistory(),
  routes: [
    ...home,
    ...authentication,
    {
      path: "/app",
      children: [
        ...user,
        ...lobby,
        ...match
      ],
      component: AppAuthenticated,
      beforeEnter: authenticate
    }
  ]
});
