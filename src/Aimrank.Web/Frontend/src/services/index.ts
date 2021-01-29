import { API_URL } from "@/constants";

import { HttpClient } from "@/modules/common/services/HttpClient";
import { AuthService } from "@/modules/authentication/services/AuthService";

export const httpClient = new HttpClient({
  baseUrl: API_URL,
  refreshTokenEnabled: true,
  refreshTokenEndpoint: "/identity/refresh"
});

export const authService = new AuthService(httpClient);
