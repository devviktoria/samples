import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { Joke } from '../models/joke';
import { User } from '../models/user';

import { JokeService } from '../services/joke.service';
import { TempauthService } from '../services/tempauth.service';

@Component({
  selector: 'app-joke-list-user',
  templateUrl: './joke-list-user.component.html',
  styleUrls: ['./joke-list-user.component.css'],
})
export class JokeListUserComponent implements OnInit {
  readonly jokesPerPage: number = 5;
  mode: string = 'released';
  currentPageIndex: number = 0;

  jokes: Joke[] = [];
  user!: User;

  constructor(
    private route: ActivatedRoute,
    private jokeService: JokeService,
    private tempauthService: TempauthService
  ) {
    this.route.data.subscribe((p) => (this.mode = p.mode));
  }

  ngOnInit(): void {
    this.tempauthService.getLoggedUser().subscribe((user) => {
      this.user = user;
      this.jokeService
        .getUserJokes(
          this.mode,
          this.user.email,
          this.jokesPerPage,
          this.currentPageIndex
        )
        .subscribe((jokes) => (this.jokes = jokes));
    });
  }
}
