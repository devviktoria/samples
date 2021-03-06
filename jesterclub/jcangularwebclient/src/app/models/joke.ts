import { EmotionCounter } from './emotioncounter';
import { ResponseStatistic } from './responsestatistic';

export interface Joke {
  Id: string;

  UserId: string;

  UserName: string;

  Text: string;

  Source: string;

  Copyright: string;

  CreationDate: Date;

  Tags: Array<string>;

  EmotionCounters: Array<EmotionCounter>;

  ResponseSum: number;

  ResponseStatistics: Array<ResponseStatistic>;
}
