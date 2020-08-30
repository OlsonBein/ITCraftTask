import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';

import {UserModel, BaseModel, TokensModel } from 'src/app/shared/models';

import { environment } from 'src/environments/environment';
import { BaseService } from 'src/app/shared/services/base/base.service';

@Injectable({
  providedIn: 'root'
})
export class UserService extends BaseService {
  apiUrl: string;

constructor(
  private http: HttpClient
) {
  super();
  this.apiUrl = environment.apiUrl;
  }

  getAllUsers(): Observable<Array<UserModel>> {
    return this.http.get<Array<UserModel>>(`${this.apiUrl}/user/getAll`, this.getParams()).pipe(
      tap((data: Array<UserModel>) => {
        if (data[0].errors.length !== 0) {
          return  this.checkServerErrors(data[0]);
        }
        return data;
      })
    );
  }

  logIn(model: UserModel): Observable<UserModel> {
    return this.http.post<UserModel>(`${this.apiUrl}/user/logIn`, model, this.getParams()).pipe(
      tap((data: UserModel) => {
        if (data.errors.length !== 0) {
          return this.checkServerErrors(data) as UserModel;
        }
        return data;
      })
    );
  }

  logOut(): Observable<any> {
    return this.http.get<any>(`${this.apiUrl}/user/logOut`);
  }

  registrate(model: UserModel): Observable<UserModel> {
    return this.http.post<UserModel>(`${this.apiUrl}/user/registrate`, model, this.getParams()).pipe(
      tap((data: UserModel) => {
        return this.checkServerErrors(data);
      })
    );
  }


  refreshTokens(tokens: TokensModel): Observable<TokensModel> {
    return this.http.post<TokensModel>(`${this.apiUrl}/user/refreshTokens`, {RefreshToken: tokens.refreshToken}, this.getParams());
  }
}
