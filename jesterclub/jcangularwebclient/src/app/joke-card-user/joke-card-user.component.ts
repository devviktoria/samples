import { Component, OnInit, Input } from '@angular/core';
import { Router } from '@angular/router';

import { MatDialog } from '@angular/material/dialog';

import { Joke } from '../models/joke';
import { JokeService } from '../services/joke.service';
import { JokeDeleteConfirmationComponent } from '../dialogs/joke-delete-confirmation/joke-delete-confirmation.component';

@Component({
  selector: 'app-joke-card-user',
  templateUrl: './joke-card-user.component.html',
  styleUrls: ['./joke-card-user.component.css'],
})
export class JokeCardUserComponent implements OnInit {
  @Input() joke!: Joke;

  constructor(
    private jokeService: JokeService,
    private router: Router,
    private dialog: MatDialog
  ) {}

  ngOnInit(): void {}

  releaseJoke() {
    this.joke.ReleasedDate = new Date();
    this.jokeService
      .updateJoke(this.joke)
      .subscribe((_) => this.router.navigateByUrl('/myreleasedjokes'));
  }

  deleteJoke() {
    let dialogRef = this.dialog.open(JokeDeleteConfirmationComponent, {
      height: '200px',
      width: '400px',
    });
    dialogRef.afterClosed().subscribe((result) => {
      if (result === 'delete') {
        this.jokeService.deleteJoke(this.joke.Id).subscribe((_) => {
          let url = this.router.url;
          this.router
            .navigateByUrl('/', { skipLocationChange: true })
            .then(() => this.router.navigate([url]));
        });
      }
    });
  }
}
