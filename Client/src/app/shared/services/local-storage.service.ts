import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import * as jwt_decode from 'jwt-decode';

import { CookieService } from 'ngx-cookie-service';
import { BehaviorSubject } from 'rxjs';

import { UserModel, TokensModel } from 'src/app/shared/models';

@Injectable({
  providedIn: 'root'
})
export class LocalStorageHelper {
  public static readonly userItem = 'user';
  public static readonly cartItem = 'cartItem';
  public readonly accessToken = 'AccessToken';
  public readonly refreshToken = 'refreshtoken';
  private currentUser = new BehaviorSubject<UserModel>(this.getUser());
  user = this.currentUser.asObservable();

  constructor(
    private router: Router,
    private cookieService: CookieService,
  ) {
  }

  getTokens(): TokensModel {
    const access = this.cookieService.get(this.accessToken);
    const refresh = this.cookieService.get(this.refreshToken);
    const model = new TokensModel(access, refresh);
    return model;
  }

  saveUser(user: UserModel): void {
    localStorage.setItem(LocalStorageHelper.userItem, JSON.stringify(user));
    this.currentUser.next(user);
  }

  getUser(): UserModel {
    let user = JSON.parse(localStorage.getItem(LocalStorageHelper.userItem));
    return user;
  }

  removeUser(): void {
    localStorage.removeItem(LocalStorageHelper.userItem);
  }

  isLoggedIn(): boolean {
    return localStorage.getItem(LocalStorageHelper.userItem) ? true : false;
  }
}
