import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { ReactiveFormsModule } from '@angular/forms';

import { MatButtonModule } from '@angular/material/button';
import { MatInputModule } from '@angular/material/input';
import { MatCardModule } from '@angular/material/card';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { JokeCardComponent } from './joke-card/joke-card.component';
import { JokeListComponent } from './joke-list/joke-list.component';
import { MainPageComponent } from './main-page/main-page.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { JokeUpsertComponent } from './joke-upsert/joke-upsert.component';
import { JokeListUserComponent } from './joke-list-user/joke-list-user.component';
import { JokeCardUserComponent } from './joke-card-user/joke-card-user.component';

@NgModule({
  declarations: [
    AppComponent,
    JokeCardComponent,
    JokeListComponent,
    MainPageComponent,
    JokeUpsertComponent,
    JokeListUserComponent,
    JokeCardUserComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    ReactiveFormsModule,
    MatButtonModule,
    MatInputModule,
    MatCardModule,
    BrowserAnimationsModule,
  ],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule {}
