import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';

import { UserModel, BaseModel } from 'src/app/shared/models';
import { UserService } from 'src/app/shared/services/user.service';
import { ValidationConstants } from 'src/app/shared/constants';

import { environment } from 'src/environments/environment';
import { LocalStorageHelper } from 'src/app/shared/services';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css'],
  providers: [UserService]
})
export class RegistrationComponent implements OnInit {
  userModel: UserModel;
  userForm: FormGroup;
  textPattern: string;
  passwordPattern: string;
  currentUser: UserModel;

  constructor(
    private formBuilder: FormBuilder,
    private userService: UserService,
    private router: Router,
    private localStorageHelper: LocalStorageHelper
    ) {
      this.textPattern = ValidationConstants.textPattern;
      this.passwordPattern = ValidationConstants.passwordPattern;
      this.userForm = this.formBuilder.group({
        name: ['', [Validators.required, Validators.maxLength(255), Validators.pattern(this.textPattern)]],
        login: ['', [Validators.required, Validators.maxLength(255)]],
        password: ['', [Validators.required, Validators.maxLength(255), Validators.pattern(this.passwordPattern)]],
        confirmPassword: ['', [Validators.required, Validators.maxLength(255), Validators.pattern(this.passwordPattern)]]
        });

      this.userModel = new UserModel();
     }

  ngOnInit(): void {
    this.localStorageHelper.user.subscribe(data => {
      this.currentUser = data;
    });
  }

  isLoggedIn(): boolean {
    return this.localStorageHelper.isLoggedIn();
  }

  checkPasswordConfirmation(): boolean {
    let result = this.userForm.get('password').value === this.userForm.get('confirmPassword').value ? true : false;
    if (!result) {
      this.userForm.controls.confirmPassword.setErrors(Validators.required);
    }
    return result;
  }

  registrate(): void {
    this.userModel.name = this.userForm.value.name;
    this.userModel.login = this.userForm.value.login;
    this.userModel.password = this.userForm.value.password;
    this.userService.registrate(this.userModel).subscribe((data: UserModel) => {
      if (data.id !== null) {

        this.localStorageHelper.saveUser(data);
        this.router.navigate(['/user/users']);
      }
    });
  }
}
