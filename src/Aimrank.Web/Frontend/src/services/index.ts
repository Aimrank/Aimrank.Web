import { API_URL } from "~/constants";

import { HttpClient } from "@/common/services/HttpClient";
import { UserService } from "@/user/services/UserService";
import { AuthService } from "@/authentication/services/AuthService";
import { SteamService } from "@/user/services/SteamService";
import { LobbyService } from "@/lobby/services/LobbyService";
import { MatchService } from "@/match/services/MatchService";
import { StatsService } from "@/match/services/StatsService";

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

export const generalHub = new GeneralHub(new Hub("/hubs/general"));
export const lobbyHub = new LobbyHub(new Hub("/hubs/lobby"));
