import { Component, OnInit } from '@angular/core';
import { FormGroup, Validators, FormControl, FormBuilder } from '@angular/forms';
import { Router } from '@angular/router';

import { UserModel } from 'src/app/shared/models';
import { UserService} from 'src/app/shared/services/user.service';

import { ValidationConstants } from 'src/app/shared/constants';
import { LocalStorageHelper } from 'src/app/shared/services/local-storage.service';

@Component({
  selector: 'app-log-in',
  templateUrl: './log-in.component.html',
  styleUrls: ['./log-in.component.css'],
  providers: [UserService]
})

export class LogInComponent implements OnInit {
  logInForm: FormGroup;
  userModel: UserModel;
  textPattern: string;
  passwordPattern: string;
  currentUser: UserModel;

constructor(
  private userService: UserService,
  private localStorageHelper: LocalStorageHelper,
  private router: Router,
  private fb: FormBuilder
  ) {
    this.textPattern = ValidationConstants.textPattern;
    this.passwordPattern = ValidationConstants.passwordPattern;
    this.userModel = new UserModel();
    this.currentUser = new UserModel();
    this.logInForm = this.fb.group({
      login: new FormControl('', [Validators.required, Validators.maxLength(255)]),
      password: new FormControl('', [Validators.required, Validators.maxLength(255), Validators.pattern(this.passwordPattern)]),
    });
  }

  ngOnInit(): void {
    // this.localStorageHelper.user.subscribe(data => {
    //   this.currentUser = data;
    // });
  }

  isLoggedIn(): boolean {
    return this.localStorageHelper.isLoggedIn();
  }

  logIn(): void {
    this.userService.logIn(this.logInForm.value).subscribe(
      (data: UserModel) => {
        if (data.errors.length !== 0) {
          return;
        }
        this.localStorageHelper.saveUser(data);
        this.router.navigate(['/user/users']);
      });
  }
}

