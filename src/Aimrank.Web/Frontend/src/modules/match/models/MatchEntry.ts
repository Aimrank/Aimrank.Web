import { IMatchDto } from "./IMatchDto";

export interface IMatchEntry extends IMatchDto {
  matchResult: -1 | 0 | 1;
  matchPlayerResult: {
    rating: number;
    difference: number;
  }
}

const getMatchResult = (match: IMatchDto, userId?: string) => {
  const p1 = match.teamTerrorists[0];
  const p2 = match.teamCounterTerrorists[0];

  if (p1.score == p2.score) {
    return 0;
  }

  const winner = p1.score > p2.score ? p1.id : p2.id;

  return winner === userId ? 1 : -1;
}

const getMatchPlayerResult = (match: IMatchDto, userId?: string) => {
  const player = [...match.teamTerrorists, ...match.teamCounterTerrorists].find(p => p.id === userId);

  if (player) {
    return { rating: player?.ratingEnd, difference: player?.ratingEnd - player?.ratingStart };
  }

  return { rating: 0, difference: 0 };
}

export const getMatchEntries = (matches: IMatchDto[], userId?: string): IMatchEntry[] => matches.map(m =>
  ({
    ...m,
    matchResult: getMatchResult(m, userId),
    matchPlayerResult: getMatchPlayerResult(m, userId)
  })
);