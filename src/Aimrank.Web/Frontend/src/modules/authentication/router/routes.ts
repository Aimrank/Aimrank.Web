import { RouteRecordRaw } from "vue-router";
import SignIn from "../views/SignIn";
import SignUp from "../views/SignUp";

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
