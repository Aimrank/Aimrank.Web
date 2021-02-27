import { MatchMode } from "./MatchMode";
import { MatchTeam } from "./MatchTeam";
import { MatchWinner } from "./MatchWinner";

interface IMatchPlayerDto {
  id: string;
  username: string;
  team: MatchTeam;
  kills: number;
  assists: number;
  deaths: number;
  score: number;
  hsPercentage: number;
  ratingStart: number;
  ratingEnd: number;
}

export interface IMatchDto {
  id: string;
  mode: MatchMode;
  winner: MatchWinner;
  scoreT: number;
  scoreCT: number;
  createdAt: string;
  finishedAt: string;
  map: string;
  teamTerrorists: IMatchPlayerDto[];
  teamCounterTerrorists: IMatchPlayerDto[];
}
