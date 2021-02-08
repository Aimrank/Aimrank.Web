import { createRouter, createWebHistory } from "vue-router";

import { routes as home } from "@/modules/home";
import { routes as authentication, authenticate } from "@/modules/authentication";
import { routes as user } from "@/modules/user";
import { routes as lobby } from "@/modules/lobby";
import { routes as match } from "@/modules/match";

import AppAuthenticated from "@/AppAuthenticated.vue";

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
