import { Component } from '@angular/core';

import { User } from './models/user';
import { TempauthService } from './services/tempauth.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent {
  title = 'Jester Club';
  currentUser?: User = undefined;
  constructor(private tempauthService: TempauthService) {}

  ngOnInit(): void {
    this.tempauthService
      .getLoggedUser()
      .subscribe((user) => (this.currentUser = user));
  }
}
