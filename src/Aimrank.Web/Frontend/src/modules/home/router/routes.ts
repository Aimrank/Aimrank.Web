import { RouteRecordRaw } from "vue-router";
import Home from "@/home/views/Home";

export const routes: RouteRecordRaw[] = [
  {
    name: "home",
    path: "/",
    component: Home
  }
];
