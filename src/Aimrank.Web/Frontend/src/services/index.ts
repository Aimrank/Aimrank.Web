import { API_URL } from "@/constants";

import { HttpClient } from "@/modules/common/services/HttpClient";
import { UserService } from "@/modules/user/services/UserService";
import { AuthService } from "@/modules/authentication/services/AuthService";
import { SteamService } from "@/modules/user/services/SteamService";
import { LobbyService } from "@/modules/lobby/services/LobbyService";
import { MatchService } from "@/modules/match/services/MatchService";

import { GeneralHub } from "@/modules/common/hubs/GeneralHub";

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

export const generalHub = new GeneralHub("/hubs/general");
