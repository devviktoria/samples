import { Component, OnInit, Input } from '@angular/core';
import { Joke } from '../models/joke';
import { ResponseStatistic } from '../models/responsestatistic';
import { JokeService } from '../services/joke.service';

@Component({
  selector: 'app-joke-card',
  templateUrl: './joke-card.component.html',
  styleUrls: ['./joke-card.component.css'],
})
export class JokeCardComponent implements OnInit {
  emotionEmoji: { [key: string]: string } = {
    sleepy: '&#x1F634;',
    none: '&#x1F610;',
    happy: '&#x1F642;',
    lol: '&#x1F600;',
    lshic: '&#x1F602;',
    invalid: '&#x2753;',
  };

  barChartHeight: number = 16;
  barChartColumnWidth: number = 8;
  barChartSpacing: number = 5;

  @Input() joke!: Joke;

  constructor(private jokeService: JokeService) {}

  ngOnInit(): void {}

  getEmojiForEmotion(value: string): string {
    let emoji = this.emotionEmoji.invalid;

    if (this.emotionEmoji[value] !== undefined) {
      emoji = this.emotionEmoji[value];
    }

    return emoji;
  }

  getResponsePercentage(responseStatistic: ResponseStatistic): number {
    if (this.joke.ResponseSum < 1) {
      return 0;
    }

    return responseStatistic.Counter / this.joke.ResponseSum;
  }

  incrementEmotionCounter(emotion: string) {
    this.jokeService
      .incrementEmotionCounter(this.joke.Id, emotion)
      .subscribe((joke) => (this.joke = joke));
  }
}
