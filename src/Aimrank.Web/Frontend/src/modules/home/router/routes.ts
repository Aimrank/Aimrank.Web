import { RouteRecordRaw } from "vue-router";
import Home from "../views/Home";

export const routes: RouteRecordRaw[] = [
  {
    name: "home",
    path: "/",
    component: Home
  }
];
