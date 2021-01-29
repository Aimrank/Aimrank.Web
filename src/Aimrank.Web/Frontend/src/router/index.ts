import { createRouter, createWebHistory } from "vue-router";

import { routes as home } from "@/modules/home";
import { routes as authentication } from "@/modules/authentication";

export const router = createRouter({
  history: createWebHistory(),
  routes: [
    ...home,
    ...authentication
  ]
});
