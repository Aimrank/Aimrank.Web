import { RouteRecordRaw } from "vue-router";
import SignIn from "@/authentication/views/SignIn";
import SignUp from "@/authentication/views/SignUp";

export const routes: RouteRecordRaw[] = [
  {
    name: "sign-up",
    path: "/sign-up",
    component: SignUp
  },
  {
    name: "sign-in",
    path: "/sign-in",
    component: SignIn
  }
];
