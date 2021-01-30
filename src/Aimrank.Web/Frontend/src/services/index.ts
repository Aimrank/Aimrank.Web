import { API_URL } from "@/constants";

import { HttpClient } from "@/modules/common/services/HttpClient";
import { UserService } from "@/modules/user/services/UserService";
import { AuthService } from "@/modules/authentication/services/AuthService";
import { SteamService } from "@/modules/user/services/SteamService";

export const httpClient = new HttpClient({
  baseUrl: API_URL,
  refreshTokenEnabled: true,
  refreshTokenEndpoint: "/identity/refresh"
});

export const userService = new UserService(httpClient);
export const authService = new AuthService(httpClient);
export const steamService = new SteamService(httpClient);
