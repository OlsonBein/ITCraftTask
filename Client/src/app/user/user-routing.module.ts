import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { GetAllUsersComponent } from 'src/app/user/users/users.component';

export const routes: Routes = [
  {path: 'users', component: GetAllUsersComponent},
  ];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class UserRoutingModule { }
