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
  joke: Joke = this.getEmptyJoke();
  currentUser?: User = undefined;

  jokeForm = this.formBuilder.group({
    id: [this.joke.Id],
    text: [this.joke.Text, Validators.required],
    source: [this.joke.Source],
    copyright: [this.joke.Copyright],
  });

  constructor(
    private formBuilder: FormBuilder,
    private tempauthService: TempauthService,
    private jokeService: JokeService,
    private router: Router
  ) {}

  public get FormTitle(): string {
    if (this.joke.Id.length === 0) {
      return 'New joke';
    }

    return 'Modify joke';
  }

  public get SaveTitle(): string {
    if (this.joke.Id.length === 0) {
      return 'Save as draft';
    }

    return 'Overwrite draft';
  }

  ngOnInit(): void {
    this.tempauthService.getLoggedUser().subscribe((user) => {
      this.currentUser = user;
      this.joke.UserEmail = user.email;
      this.joke.UserName = user.name;
    });
  }

  onSubmit(event: Event) {
    this.updateJokeFromFormData();

    let submitEvent: CustomSubmitEvent = event as CustomSubmitEvent;
    let submitterName: string = submitEvent.submitter.getAttribute(
      'name'
    ) as string;

    if (submitterName === 'release') {
      this.joke.ReleasedDate = new Date();
    }

    if (this.joke.Id.length === 0) {
      this.joke.CreationDate = new Date();
      this.jokeService.addJoke(this.joke).subscribe((joke) => {
        this.joke = joke;
        this.updateFormDataFromJoke();
      });
    } else {
      this.jokeService.updateJoke(this.joke).subscribe();
    }
    //this.router.navigateByUrl('/mainpage');
  }

  getEmptyJoke(): Joke {
    return {
      Id: '',
      UserEmail: '',
      UserName: '',
      Text: '',
      Source: '',
      Copyright: '',
      Tags: [],
    };
  }

  updateJokeFromFormData() {
    this.joke.Text = this.jokeForm.controls['text'].value;
    this.joke.Source = this.jokeForm.controls['source'].value;
    this.joke.Copyright = this.jokeForm.controls['copyright'].value;
  }

  updateFormDataFromJoke() {
    this.jokeForm.setValue({
      id: this.joke.Id,
      text: this.joke.Text,
      source: this.joke.Source,
      copyright: this.joke.Copyright,
    });
    console.debug(this.joke.Id);
  }
}

interface CustomSubmitEvent extends Event {
  submitter: HTMLElement;
}
