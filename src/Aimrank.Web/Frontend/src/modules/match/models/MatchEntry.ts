import { IMatchDto } from "./IMatchDto";
import { MatchWinner } from "./MatchWinner";

export interface IMatchEntry extends IMatchDto {
  matchResult: -1 | 0 | 1;
  matchPlayerResult: {
    kills: number;
    deaths: number;
    rating: number;
    difference: number;
    hsPercentage: number;
  } | null;
}

const getMatchResult = (match: IMatchDto, userId?: string) => {
  if (match.winner === MatchWinner.Draw) {
    return 0;
  }

  if (match.winner === MatchWinner.T && match.teamTerrorists.some(m => m.id === userId) ||
      match.winner === MatchWinner.CT && match.teamCounterTerrorists.some(m => m.id === userId))
  {
    return 1;
  }

  return -1;
}

const getMatchPlayerResult = (match: IMatchDto, userId?: string) => {
  const player = [...match.teamTerrorists, ...match.teamCounterTerrorists].find(p => p.id === userId);

  if (player) {
    return {
      kills: player.kills,
      deaths: player.deaths,
      rating: player.ratingEnd,
      difference: player.ratingEnd - player.ratingStart,
      hsPercentage: player.hsPercentage
    };
  }

  return null;
}

export const getMatchEntries = (matches: IMatchDto[], userId?: string): IMatchEntry[] => matches.map(m =>
  ({
    ...m,
    matchResult: getMatchResult(m, userId),
    matchPlayerResult: getMatchPlayerResult(m, userId)
  })
);