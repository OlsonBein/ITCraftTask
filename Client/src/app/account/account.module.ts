import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { RegistrationComponent } from 'src/app/account/registration/registration.component';
import { LogInComponent } from 'src/app/account/log-in/log-in.component';

import {routes} from 'src/app/account/account-routing.module';

@NgModule({
  declarations: [
  RegistrationComponent,
  LogInComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forChild(routes)
  ]
})
export class AccountModule { }
