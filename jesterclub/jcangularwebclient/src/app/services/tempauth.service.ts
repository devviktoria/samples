import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';

import { User } from '../models/user';

@Injectable({
  providedIn: 'root',
})
export class TempauthService {
  private user: User;

  constructor() {
    this.user = { name: 'devviktoria', email: 'devviktoria@email.com' };
  }

  getLoggedUser(): Observable<User> {
    return new Observable((subscriber) => {
      subscriber.next(this.user);
    });
  }
}
