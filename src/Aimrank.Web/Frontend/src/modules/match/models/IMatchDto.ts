import { MatchMode } from "./MatchMode";
import { MatchStatus } from "./MatchStatus";

export interface IMatchDto {
  id: string;
  map: string;
  mode: MatchMode;
  status: MatchStatus;
  address: string;
}