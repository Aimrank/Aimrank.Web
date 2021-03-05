import { MatchMode } from "./MatchMode";

export interface IUserStatsDto {
  matchesTotal: number;
  matchesWon: number;
  totalKills: number;
  totalDeaths: number;
  totalHs: number;
  modes: IUserStatsModeDto[];
}

interface IUserStatsModeDto {
  mode: MatchMode;
  matchesTotal: number;
  matchesWon: number;
  totalKills: number;
  totalDeaths: number;
  totalHs: number;
  maps: IUserStatsMapDto[];
}

interface IUserStatsMapDto {
  map: string;
  matchesTotal: number;
  matchesWon: number;
  totalKills: number;
  totalDeaths: number;
  totalHs: number;
}
