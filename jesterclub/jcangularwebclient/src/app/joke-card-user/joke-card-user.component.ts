import { Component, OnInit, Input } from '@angular/core';
import { Joke } from '../models/joke';

@Component({
  selector: 'app-joke-card-user',
  templateUrl: './joke-card-user.component.html',
  styleUrls: ['./joke-card-user.component.css'],
})
export class JokeCardUserComponent implements OnInit {
  @Input() joke!: Joke;

  constructor() {}

  ngOnInit(): void {}
}
