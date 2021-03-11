import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import {
  FormBuilder,
  FormGroup,
  FormControl,
  Validators,
} from '@angular/forms';

import { Joke } from '../models/joke';
import { User } from '../models/user';
import { TempauthService } from '../services/tempauth.service';
import { JokeService } from '../services/joke.service';

@Component({
  selector: 'app-joke-upsert',
  templateUrl: './joke-upsert.component.html',
  styleUrls: ['./joke-upsert.component.css'],
})
export class JokeUpsertComponent implements OnInit {
  currentUser?: User = undefined;

  jokeForm = this.fb.group({
    text: ['', Validators.required],
    source: [''],
    copyright: [''],
  });

  constructor(
    private fb: FormBuilder,
    private tempauthService: TempauthService,
    private jokeService: JokeService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.tempauthService
      .getLoggedUser()
      .subscribe((user) => (this.currentUser = user));
  }

  onSubmit() {
    let joke: Joke = {
      UserEmail: this.currentUser?.email as string,
      UserName: this.currentUser?.name as string,
      Text: this.jokeForm.controls['text'].value,
      Source: this.jokeForm.controls['source'].value,
      Copyright: this.jokeForm.controls['copyright'].value,
      Tags: [],
    };

    this.jokeService.addJoke(joke).subscribe();
    this.router.navigateByUrl('/mainpage');
  }
}
