import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MainPageComponent } from './main-page/main-page.component';
import { JokeUpsertComponent } from './joke-upsert/joke-upsert.component';
import { JokeListUserComponent } from './joke-list-user/joke-list-user.component';

const routes: Routes = [
  { path: '', redirectTo: '/mainpage', pathMatch: 'full' },
  { path: 'mainpage', component: MainPageComponent },
  { path: 'jokeupsert', component: JokeUpsertComponent },
  {
    path: 'mydrafjokes',
    component: JokeListUserComponent,
    data: { mode: 'draft' },
  },
  {
    path: 'myreleasedjokes',
    component: JokeListUserComponent,
    data: { mode: 'released' },
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
