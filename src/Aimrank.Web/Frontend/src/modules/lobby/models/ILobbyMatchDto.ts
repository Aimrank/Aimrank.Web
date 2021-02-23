import { MatchMode } from "@/match/models/MatchMode";
import { MatchStatus } from "@/match/models/MatchStatus";

export interface ILobbyMatchDto {
  id: string;
  map: string;
  mode: MatchMode;
  status: MatchStatus;
  address: string;
}