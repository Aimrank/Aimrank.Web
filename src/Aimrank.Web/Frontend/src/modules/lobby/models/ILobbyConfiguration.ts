import { MatchMode } from "@/profile/models/MatchMode";

export interface ILobbyConfiguration {
  name: string;
  map: string;
  mode: MatchMode;
}
