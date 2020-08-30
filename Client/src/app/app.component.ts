import { Component } from '@angular/core';
import { LocalStorageHelper, UserService } from './shared/services';
import { Router } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent {

  constructor(
    private localStorage: LocalStorageHelper,
    private userService: UserService,
    private router: Router
    ) {
     }

    isLoggedIn(): boolean {
    return this.localStorage.isLoggedIn();
  }

  logOut() {
    this.userService.logOut().subscribe();
    this.localStorage.removeUser();
    this.router.navigate(['/user/users']);

  }
}

