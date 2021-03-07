import { Component, OnInit, Input } from '@angular/core';
import { Observable, of } from 'rxjs';

import { Joke } from '../models/joke';
import { JokeService } from '../services/joke.service';

@Component({
  selector: 'app-joke-list',
  templateUrl: './joke-list.component.html',
  styleUrls: ['./joke-list.component.css'],
})
export class JokeListComponent implements OnInit {
  @Input() jokeListType: string = 'latest';

  jokes: Joke[] = [];
  constructor(private jokeService: JokeService) {}

  ngOnInit(): void {
    if (this.jokeListType === 'latest') {
      this.getLatestJokes();
    } else {
      this.getMostPopularJokes();
    }
  }

  getLatestJokes(): void {
    this.jokeService
      .getLatestJokes()
      .subscribe((jokes) => (this.jokes = jokes));
  }

  getMostPopularJokes(): void {
    this.jokeService
      .getMostPopularJokes()
      .subscribe((jokes) => (this.jokes = jokes));
  }
}
