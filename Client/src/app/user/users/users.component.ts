import { Component, OnInit } from '@angular/core';
import { UserModel } from 'src/app/shared/models';
import { UserService } from 'src/app/shared/services';

@Component({
  selector: 'app-get-all-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.css'],
  providers: [UserService]
})
export class GetAllUsersComponent implements OnInit {
  users: Array<UserModel>;

  constructor(
    private userService: UserService,
    ) {
     }

  ngOnInit(): void {
    this.getUsers();
  }


  getUsers(): void {
    this.userService.getAllUsers().subscribe((data: Array<UserModel>) => {
      this.users = data;
      });
  }
}

