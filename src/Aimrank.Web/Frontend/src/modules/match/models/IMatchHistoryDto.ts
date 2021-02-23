import { MatchMode } from "./MatchMode";
import { MatchTeam } from "./MatchTeam";

interface IMatchHistoryPlayerDto {
  id: string;
  username: string;
  team: MatchTeam;
  kills: number;
  assists: number;
  deaths: number;
  score: number;
  ratingStart: number;
  ratingEnd: number;
}

export interface IMatchHistoryDto {
  id: string;
  mode: MatchMode;
  scoreT: number;
  scoreCT: number;
  createdAt: string;
  finishedAt: string;
  map: string;
  teamTerrorists: IMatchHistoryPlayerDto[];
  teamCounterTerrorists: IMatchHistoryPlayerDto[];
}
