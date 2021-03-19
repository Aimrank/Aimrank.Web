import { IMatchDto } from "./IMatchDto";

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
