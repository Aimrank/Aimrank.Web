import { MatchMode } from "@/profile/models/MatchMode";
import { MatchStatus } from "@/profile/models/MatchStatus";

export interface ILobbyMatchDto {
  id: string;
  map: string;
  mode: MatchMode;
  status: MatchStatus;
  address: string;
}