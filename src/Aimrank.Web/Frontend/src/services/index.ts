import { API_URL } from "~/constants";

import { HttpClient } from "@/common/services/HttpClient";
import { AuthService } from "@/authentication/services/AuthService";
import { UserService } from "@/profile/services/UserService";
import { SteamService } from "@/profile/services/SteamService";
import { StatsService } from "@/profile/services/StatsService";
import { MatchService } from "@/profile/services/MatchService";
import { LobbyService } from "@/lobby/services/LobbyService";
import { FriendshipService } from "@/profile/services/FriendshipService";

import { Hub } from "@/common/hubs/Hub";
import { GeneralHub } from "@/common/hubs/GeneralHub";
import { LobbyHub } from "@/lobby/hubs/LobbyHub";

export const httpClient = new HttpClient({
  baseUrl: API_URL,
  refreshTokenEnabled: true,
  refreshTokenEndpoint: "/identity/refresh"
});

export const userService = new UserService(httpClient);
export const authService = new AuthService(httpClient);
export const steamService = new SteamService(httpClient);
export const lobbyService = new LobbyService(httpClient);
export const matchService = new MatchService(httpClient);
export const statsService = new StatsService(httpClient);
export const friendshipService = new FriendshipService(httpClient);

export const generalHub = new GeneralHub(new Hub("/hubs/general"));
export const lobbyHub = new LobbyHub(new Hub("/hubs/lobby"));
