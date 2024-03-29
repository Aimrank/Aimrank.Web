import { RouteRecordRaw } from "vue-router";
import SignIn from "@/authentication/views/SignIn";
import SignUp from "@/authentication/views/SignUp";
import SignUpSuccess from "@/authentication/views/SignUpSuccess";
import ResetPassword from "@/authentication/views/ResetPassword";

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
  },
  {
    name: "sign-up-success",
    path: "/sign-up/success",
    component: SignUpSuccess
  },
  {
    name: "reset-password",
    path: "/reset-password",
    component: ResetPassword
  }
];
