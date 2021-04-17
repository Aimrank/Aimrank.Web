import { RouteRecordRaw } from "vue-router";
import AppHome from "@/app/views/AppHome";

export const routes: RouteRecordRaw[] = [
  {
    name: "app",
    path: "",
    component: AppHome
  }
];
