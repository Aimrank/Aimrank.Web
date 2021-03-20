import { createRouter, createWebHistory } from "vue-router";

import { authenticate } from "@/authentication/router/authenticationGuard";

import { routes as app } from "@/app/router/routes";
import { routes as authentication } from "@/authentication/router/routes";
import { routes as home } from "@/home/router/routes";
import { routes as lobby } from "@/lobby/router/routes";
import { routes as profile } from "@/profile/router/routes";

import AppAuthenticated from "~/AppAuthenticated.vue";

export const router = createRouter({
  history: createWebHistory(),
  routes: [
    ...home,
    ...authentication,
    {
      path: "/app",
      children: [
        ...app,
        ...lobby,
        ...profile
      ],
      component: AppAuthenticated,
      beforeEnter: authenticate
    }
  ]
});
