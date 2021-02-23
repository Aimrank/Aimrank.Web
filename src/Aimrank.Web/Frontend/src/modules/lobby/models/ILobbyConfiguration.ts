import { MatchMode } from "@/match/models/MatchMode";

export interface ILobbyConfiguration {
  name: string;
  map: string;
  mode: MatchMode;
}
