import { Component, OnInit } from '@angular/core';

import { User } from '../models/user';
import { TempauthService } from '../services/tempauth.service';

@Component({
  selector: 'app-main-page',
  templateUrl: './main-page.component.html',
  styleUrls: ['./main-page.component.css'],
})
export class MainPageComponent implements OnInit {
  currentUser?: User = undefined;
  constructor(private tempauthService: TempauthService) {}

  ngOnInit(): void {
    this.tempauthService
      .getLoggedUser()
      .subscribe((user) => (this.currentUser = user));
  }
}
