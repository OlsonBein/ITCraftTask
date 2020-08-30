import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { UserRoutingModule } from 'src/app/user/user-routing.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import {SharedModule } from 'src/app/shared/shared.module';

import { GetAllUsersComponent } from 'src/app/user/users/users.component';

import { routes } from 'src/app/user/user-routing.module';


@NgModule({
  declarations: [
    GetAllUsersComponent,
    ],
  imports: [
    SharedModule,
    CommonModule,
    UserRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forChild(routes)
  ]
})
export class UserModule { }
