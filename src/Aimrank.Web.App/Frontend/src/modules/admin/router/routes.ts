import { RouteRecordRaw } from "vue-router";
import { authenticateRoles } from "@/authentication/router/authenticationGuard";
import Layout from "@/admin/views/Layout";
import Dashboard from "@/admin/views/Dashboard";
import SteamTokens from "@/admin/views/SteamTokens";

export const routes: RouteRecordRaw[] = [
  {
    name: "admin",
    path: "admin",
    component: Layout,
    beforeEnter: authenticateRoles("Admin"),
    children: [
      {
        name: "admin:dashboard",
        path: "",
        component: Dashboard
      },
      {
        name: "admin:steamTokens",
        path: "steam-tokens",
        component: SteamTokens
      }
    ]
  }
];
