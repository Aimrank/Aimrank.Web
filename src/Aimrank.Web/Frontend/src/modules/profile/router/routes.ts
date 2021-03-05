import { RouteRecordRaw } from "vue-router";
import Settings from "@/profile/views/Settings";
import Matches from "@/profile/views/Matches";
import Profile from "@/profile/views/Profile";

export const routes: RouteRecordRaw[] = [
  {
    name: "profile",
    path: "profile/:userId",
    component: Profile,
    children: [
      {
        name: "settings",
        path: "settings",
        component: Settings
      },
      {
        name: "matches",
        path: "matches",
        component: Matches
      }
    ]
  },
];
