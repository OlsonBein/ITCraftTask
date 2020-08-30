import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { RegistrationComponent } from 'src/app/account/registration/registration.component';
import { LogInComponent } from 'src/app/account/log-in/log-in.component';

export const routes: Routes = [
    {path: 'logIn', component: LogInComponent},
    {path: 'registration', component: RegistrationComponent},
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AccountRoutingModule { }
