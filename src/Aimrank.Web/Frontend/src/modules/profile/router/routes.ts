import { RouteRecordRaw } from "vue-router";
import Settings from "@/profile/views/Settings";
import Matches from "@/profile/views/Matches";
import Profile from "@/profile/views/Profile";
import Friends from "@/profile/views/Friends";

export const routes: RouteRecordRaw[] = [
  {
    name: "profile",
    path: "profile/:userId?",
    component: Profile,
    children: [
      {
        name: "matches",
        path: "matches",
        component: Matches
      },
      {
        name: "friends",
        path: "friends",
        component: Friends
      }
    ]
  },
  {
    name: "settings",
    path: "settings",
    component: Settings
  }
];
