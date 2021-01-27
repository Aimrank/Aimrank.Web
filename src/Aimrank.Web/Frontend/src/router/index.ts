import { createRouter, createWebHistory } from "vue-router";

import Home from "@/views/Home";

export const router = createRouter({
  history: createWebHistory(),
  routes: [
    {
      name: "home",
      path: "/",
      component: Home
    }
  ]
});
