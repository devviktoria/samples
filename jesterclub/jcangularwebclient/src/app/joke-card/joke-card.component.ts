import { Component, OnInit, Input } from '@angular/core';
import { Joke } from '../models/joke';

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

  constructor() {}

  ngOnInit(): void {
    this.joke = {
      Id: 'string',

      UserId: '12346',

      UserName: 'test1',

      Text: 'joke1',

      Source: 'string',

      Copyright: 'lopy',

      CreationDate: new Date(),

      Tags: ['tag1', 'tag2'],

      EmotionCounters: [
        { Emotion: 'sleepy', Counter: 5 },
        { Emotion: 'none', Counter: 5 },
        { Emotion: 'happy', Counter: 2 },
        { Emotion: 'lol', Counter: 5 },
        { Emotion: 'lshic', Counter: 5 },
      ],

      ResponseSum: 7,

      ResponseStatistics: [
        { Day: 0, Counter: 3 },
        { Day: 1, Counter: 1 },
        { Day: 2, Counter: 2 },
        { Day: 3, Counter: 1 },
        { Day: 4, Counter: 0 },
        { Day: 5, Counter: 0 },
        { Day: 6, Counter: 0 },
      ],
    };
  }

  getEmojiForEmotion(value: string): string {
    let emoji = this.emotionEmoji.invalid;

    if (this.emotionEmoji[value] !== undefined) {
      emoji = this.emotionEmoji[value];
    }

    return emoji;
  }
}
