import { HttpClient } from "@/common/services/HttpClient";
import { UserService } from "@/profile/services/UserService";
import { SteamService } from "@/profile/services/SteamService";
import { MatchService } from "@/profile/services/MatchService";

import { Hub } from "@/common/hubs/Hub";
import { GeneralHub } from "@/common/hubs/GeneralHub";
import { LobbyHub } from "@/lobby/hubs/LobbyHub";

export const httpClient = new HttpClient();

export const userService = new UserService(httpClient);
export const steamService = new SteamService(httpClient);
export const matchService = new MatchService(httpClient);

export const generalHub = new GeneralHub(new Hub("/hubs/general"));
export const lobbyHub = new LobbyHub(new Hub("/hubs/lobby"));
